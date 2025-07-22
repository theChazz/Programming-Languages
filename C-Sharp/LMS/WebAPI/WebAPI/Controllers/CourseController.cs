using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Data.SqlClient;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public CourseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = new List<CourseResponse>();
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand(
                        "SELECT id, course_name, description, category, difficulty, syllabus, prerequisites, created_at FROM courses",
                        connection);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            courses.Add(new CourseResponse
                            {
                                Id = reader.GetInt32(0),
                                CourseName = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Category = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Difficulty = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Syllabus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Prerequisites = reader.IsDBNull(6) ? null : reader.GetString(6),
                                CreatedAt = reader.GetDateTime(7)
                            });
                        }
                    }
                }
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching courses");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand(
                        "SELECT id, course_name, description, category, difficulty, syllabus, prerequisites, created_at FROM courses WHERE id = @Id",
                        connection);
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var course = new CourseResponse
                            {
                                Id = reader.GetInt32(0),
                                CourseName = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Category = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Difficulty = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Syllabus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Prerequisites = reader.IsDBNull(6) ? null : reader.GetString(6),
                                CreatedAt = reader.GetDateTime(7)
                            };
                            return Ok(course);
                        }
                    }
                }
                return NotFound("Course not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the course");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand(
                        @"INSERT INTO courses (course_name, description, category, difficulty, syllabus, prerequisites)
                          VALUES (@CourseName, @Description, @Category, @Difficulty, @Syllabus, @Prerequisites);
                          SELECT SCOPE_IDENTITY();",
                        connection);

                    cmd.Parameters.AddWithValue("@CourseName", request.CourseName);
                    cmd.Parameters.AddWithValue("@Description", (object)request.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Category", (object)request.Category ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Difficulty", (object)request.Difficulty ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Syllabus", (object)request.Syllabus ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Prerequisites", (object)request.Prerequisites ?? DBNull.Value);

                    var id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return Ok(new { Id = id, Message = "Course created successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the course");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    var cmd = new SqlCommand(
                        @"UPDATE courses 
                          SET course_name = @CourseName,
                              description = @Description,
                              category = @Category,
                              difficulty = @Difficulty,
                              syllabus = @Syllabus,
                              prerequisites = @Prerequisites
                          WHERE id = @Id",
                        connection);

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@CourseName", request.CourseName);
                    cmd.Parameters.AddWithValue("@Description", (object)request.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Category", (object)request.Category ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Difficulty", (object)request.Difficulty ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Syllabus", (object)request.Syllabus ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Prerequisites", (object)request.Prerequisites ?? DBNull.Value);

                    var rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected == 0)
                    {
                        return NotFound("Course not found");
                    }

                    return Ok(new { Message = "Course updated successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the course");
            }
        }
    }
} 