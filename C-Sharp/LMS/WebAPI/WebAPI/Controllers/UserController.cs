using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public UserController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    // Check if email already exists
                    var checkEmailCmd = new SqlCommand(
                        "SELECT COUNT(1) FROM users WHERE email = @Email",
                        connection);
                    checkEmailCmd.Parameters.AddWithValue("@Email", request.Email);
                    var emailExists = (int)await checkEmailCmd.ExecuteScalarAsync() > 0;

                    if (emailExists)
                    {
                        return BadRequest(new AuthResponse 
                        { 
                            Success = false, 
                            Message = "Email already exists" 
                        });
                    }

                    // Hash password
                    var passwordHash = HashPassword(request.Password);

                    // Insert new user
                    var cmd = new SqlCommand(
                        @"INSERT INTO users (full_name, email, password_hash, role) 
                          VALUES (@FullName, @Email, @PasswordHash, @Role);
                          SELECT SCOPE_IDENTITY();",
                        connection);

                    cmd.Parameters.AddWithValue("@FullName", request.FullName);
                    cmd.Parameters.AddWithValue("@Email", request.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@Role", request.Role);

                    var userId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    return Ok(new AuthResponse
                    {
                        Success = true,
                        Message = "Registration successful",
                        User = new UserResponse
                        {
                            Id = userId,
                            FullName = request.FullName,
                            Email = request.Email,
                            Role = request.Role
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AuthResponse 
                { 
                    Success = false, 
                    Message = "An error occurred during registration" + ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    var cmd = new SqlCommand(
                        "SELECT id, full_name, email, password_hash, role FROM users WHERE email = @Email",
                        connection);
                    cmd.Parameters.AddWithValue("@Email", request.Email);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var storedHash = reader.GetString(3);
                            if (VerifyPassword(request.Password, storedHash))
                            {
                                return Ok(new AuthResponse
                                {
                                    Success = true,
                                    Message = "Login successful",
                                    User = new UserResponse
                                    {
                                        Id = reader.GetInt32(0),
                                        FullName = reader.GetString(1),
                                        Email = reader.GetString(2),
                                        Role = reader.GetString(4)
                                    }
                                });
                            }
                        }
                    }

                    return BadRequest(new AuthResponse 
                    { 
                        Success = false, 
                        Message = "Invalid email or password" 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AuthResponse 
                { 
                    Success = false, 
                    Message = "An error occurred during login" 
                });
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
} 