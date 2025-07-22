using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Data.SqlClient;
using WebAPI.Services;
using System.Security.Cryptography;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private static readonly string[] ValidRoles = { "admin", "lecturer", "learner", "support_staff" };

        public AdminController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet("user-stats")]
        public async Task<IActionResult> GetUserStats()
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var stats = new
                    {
                        totalUsers = 0,
                        admins = 0,
                        learners = 0,
                        lecturers = 0,
                        supportStaff = 0
                    };

                    var cmd = new SqlCommand(@"
                        SELECT 
                            COUNT(*) as TotalUsers,
                            SUM(CASE WHEN role = 'admin' THEN 1 ELSE 0 END) as Admins,
                            SUM(CASE WHEN role = 'learner' THEN 1 ELSE 0 END) as Learners,
                            SUM(CASE WHEN role = 'lecturer' THEN 1 ELSE 0 END) as Lecturers,
                            SUM(CASE WHEN role = 'support_staff' THEN 1 ELSE 0 END) as SupportStaff
                        FROM users", connection);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            stats = new
                            {
                                totalUsers = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                admins = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                learners = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                lecturers = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                supportStaff = reader.IsDBNull(4) ? 0 : reader.GetInt32(4)
                            };
                        }
                    }
                    return Ok(stats);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching user statistics", error = ex.Message });
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var users = new List<UserResponse>();
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    
                    // Get total count
                    var countCmd = new SqlCommand("SELECT COUNT(*) FROM users", connection);
                    var totalCount = (int)await countCmd.ExecuteScalarAsync();
                    
                    // Get paginated users
                    var offset = (page - 1) * pageSize;
                    var cmd = new SqlCommand(@"
                        SELECT id, full_name, email, role 
                        FROM users 
                        ORDER BY id 
                        OFFSET @Offset ROWS 
                        FETCH NEXT @PageSize ROWS ONLY", connection);
                    
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new UserResponse
                            {
                                Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                FullName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Role = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                            });
                        }
                    }

                    var result = new
                    {
                        totalCount,
                        totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                        currentPage = page,
                        pageSize,
                        users
                    };
                
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching users", error = ex.Message });
            }
        }

        [HttpPut("users/{id}/role")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateRoleRequest request)
        {
            if (string.IsNullOrEmpty(request?.Role))
            {
                return BadRequest(new { message = "Role is required" });
            }

            if (!ValidRoles.Contains(request.Role.ToLower()))
            {
                return BadRequest(new { message = $"Invalid role. Valid roles are: {string.Join(", ", ValidRoles)}" });
            }

            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    
                    // Check if role actually changed
                    var checkCmd = new SqlCommand(
                        "SELECT role FROM users WHERE id = @Id",
                        connection);
                    checkCmd.Parameters.AddWithValue("@Id", id);
                    
                    var currentRole = await checkCmd.ExecuteScalarAsync() as string;
                    if (currentRole == null)
                    {
                        return NotFound(new { message = "User not found" });
                    }
                    
                    if (currentRole.ToLower() == request.Role.ToLower())
                    {
                        return Ok(new { message = "Role is already set to the requested value" });
                    }

                    var cmd = new SqlCommand(
                        "UPDATE users SET role = @Role WHERE id = @Id",
                        connection);

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Role", request.Role.ToLower());

                    var rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return Ok(new { message = "User role updated successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating user role", error = ex.Message });
            }
        }

        [HttpPut("users/{id}/reset-password")]
        public async Task<IActionResult> ResetPassword(int id)
        {
            try
            {
                // Generate a secure random password
                var tempPassword = GenerateSecurePassword();
                var passwordHash = HashPasswordBcrypt(tempPassword);

                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    
                    // Check if user exists
                    var checkCmd = new SqlCommand(
                        "SELECT COUNT(1) FROM users WHERE id = @Id",
                        connection);
                    checkCmd.Parameters.AddWithValue("@Id", id);
                    
                    if ((int)await checkCmd.ExecuteScalarAsync() == 0)
                    {
                        return NotFound(new { message = "User not found" });
                    }

                    var cmd = new SqlCommand(
                        "UPDATE users SET password_hash = @PasswordHash WHERE id = @Id",
                        connection);

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    await cmd.ExecuteNonQueryAsync();

                    // Log password reset (in a real application, use proper audit logging)
                    var auditCmd = new SqlCommand(
                        "INSERT INTO notifications (user_id, message) VALUES (@UserId, 'Password was reset by admin')",
                        connection);
                    auditCmd.Parameters.AddWithValue("@UserId", id);
                    await auditCmd.ExecuteNonQueryAsync();

                    return Ok(new { 
                        message = "Password reset successfully",
                        temporaryPassword = tempPassword 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while resetting password", error = ex.Message });
            }
        }

        [HttpGet("users/{id}/details")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand(
                        "SELECT id, full_name, email, role, created_at FROM users WHERE id = @Id",
                        connection);
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var user = new UserResponse
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Email = reader.GetString(2),
                                Role = reader.GetString(3),
                                //CreatedAt = reader.GetDateTime(4)
                            };
                            return Ok(user);
                        }
                    }
                }
                return NotFound(new { message = "User not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the user" });
            }
        }

        [HttpPut("users/{id}/update")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.FullName) || string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest(new { message = "Full name and email are required" });
                }

                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    
                    // Check if email is already taken by another user
                    var checkCmd = new SqlCommand(
                        "SELECT COUNT(1) FROM users WHERE email = @Email AND id != @Id",
                        connection);
                    checkCmd.Parameters.AddWithValue("@Email", request.Email);
                    checkCmd.Parameters.AddWithValue("@Id", id);
                    
                    if ((int)await checkCmd.ExecuteScalarAsync() > 0)
                    {
                        return BadRequest(new { message = "Email is already in use" });
                    }

                    var cmd = new SqlCommand(
                        @"UPDATE users 
                          SET full_name = @FullName,
                              email = @Email
                          WHERE id = @Id",
                        connection);

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@FullName", request.FullName);
                    cmd.Parameters.AddWithValue("@Email", request.Email);

                    var rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected == 0)
                    {
                        return NotFound(new { message = "User not found" });
                    }

                    return Ok(new { message = "User updated successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the user" });
            }
        }

        private string GenerateSecurePassword()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[32];
                rng.GetBytes(bytes);
                
                var chars = new char[16];
                for (int i = 0; i < 16; i++)
                {
                    chars[i] = validChars[bytes[i] % validChars.Length];
                }
                
                return new string(chars);
            }
        }

        private string HashPasswordBcrypt(string password)
        {
            // Note: In a real application, use a proper BCrypt library
            // This is a placeholder that still uses SHA256
            // TODO: Replace with BCrypt
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
} 