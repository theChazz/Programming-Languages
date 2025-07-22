using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Data.SqlClient;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public ProgramController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrograms()
        {
            try
            {
                var programs = new List<ProgramResponse>();
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand(
                        "SELECT id, name, code, level, department, status, description, type, duration_months, created_at FROM programs",
                        connection);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            programs.Add(new ProgramResponse
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Code = reader.GetString(2),
                                Level = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Department = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Status = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Description = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Type = reader.IsDBNull(7) ? null : reader.GetString(7),
                                DurationMonths = reader.GetInt32(8),
                                CreatedAt = reader.GetDateTime(9)
                            });
                        }
                    }
                }
                return Ok(programs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching programs");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgram(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand(
                        "SELECT id, name, code, level, department, status, description, type, duration_months, created_at FROM programs WHERE id = @Id",
                        connection);
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var program = new ProgramResponse
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Code = reader.GetString(2),
                                Level = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Department = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Status = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Description = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Type = reader.IsDBNull(7) ? null : reader.GetString(7),
                                DurationMonths = reader.GetInt32(8),
                                CreatedAt = reader.GetDateTime(9)
                            };
                            return Ok(program);
                        }
                    }
                }
                return NotFound("Program not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the program");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand(
                        @"INSERT INTO programs (name, code, level, department, status, description, type, duration_months)
                          VALUES (@Name, @Code, @Level, @Department, @Status, @Description, @Type, @DurationMonths);
                          SELECT SCOPE_IDENTITY();",
                        connection);

                    cmd.Parameters.AddWithValue("@Name", request.Name);
                    cmd.Parameters.AddWithValue("@Code", request.Code);
                    cmd.Parameters.AddWithValue("@Level", (object)request.Level ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Department", (object)request.Department ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", (object)request.Status ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object)request.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Type", (object)request.Type ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DurationMonths", request.DurationMonths);

                    var id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return Ok(new { Id = id, Message = "Program created successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the program");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] ProgramRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand(
                        @"UPDATE programs 
                          SET name = @Name,
                              code = @Code,
                              level = @Level,
                              department = @Department,
                              status = @Status,
                              description = @Description,
                              type = @Type,
                              duration_months = @DurationMonths
                          WHERE id = @Id",
                        connection);

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", request.Name);
                    cmd.Parameters.AddWithValue("@Code", request.Code);
                    cmd.Parameters.AddWithValue("@Level", (object)request.Level ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Department", (object)request.Department ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", (object)request.Status ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object)request.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Type", (object)request.Type ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DurationMonths", request.DurationMonths);

                    var rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected == 0)
                    {
                        return NotFound("Program not found");
                    }

                    return Ok(new { Message = "Program updated successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the program");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand("DELETE FROM programs WHERE id = @Id", connection);
                    cmd.Parameters.AddWithValue("@Id", id);

                    var rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected == 0)
                    {
                        return NotFound("Program not found");
                    }

                    return Ok(new { Message = "Program deleted successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the program");
            }
        }
    }
} 