using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLMS.Models;
using WebApiLMS.DTOs.UserProgramEnrollment;
using WebApiLMS.Services;

namespace WebApiLMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProgramEnrollmentController : ControllerBase
    {
        private readonly UserProgramEnrollmentService _service;

        public UserProgramEnrollmentController(UserProgramEnrollmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserProgramEnrollmentModel>>> GetEnrollments()
        {
            var enrollments = await _service.GetAllEnrollmentsAsync();
            return Ok(enrollments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProgramEnrollmentModel>> GetEnrollment(int id)
        {
            var enrollment = await _service.GetEnrollmentByIdAsync(id);
            if (enrollment == null) return NotFound();
            return Ok(enrollment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment([FromBody] UserProgramEnrollmentRequest requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var enrollment = new UserProgramEnrollmentModel
                {
                    UserId = requestDto.UserId,
                    ProgramId = requestDto.ProgramId,
                    Status = requestDto.Status,
                    EnrolledAt = DateTime.UtcNow,
                    ExpectedCompletionDate = requestDto.ExpectedCompletionDate
                };

                var createdEnrollment = await _service.AddEnrollmentAsync(enrollment);
                return CreatedAtAction(nameof(GetEnrollment), new { id = createdEnrollment.Id }, createdEnrollment);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the enrollment.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, [FromBody] UserProgramEnrollmentRequest requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingEnrollment = await _service.GetEnrollmentByIdAsync(id);

                if (existingEnrollment == null)
                {
                    return NotFound($"Enrollment with ID {id} not found.");
                }

                existingEnrollment.Status = requestDto.Status;
                existingEnrollment.ExpectedCompletionDate = requestDto.ExpectedCompletionDate;

                var updatedResult = await _service.UpdateEnrollmentAsync(id, existingEnrollment);

                if (updatedResult == null)
                {
                    return NotFound($"Failed to update enrollment with ID {id}. It might have been deleted.");
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the enrollment.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var result = await _service.DeleteEnrollmentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
