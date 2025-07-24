using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiLMS.DTOs.CourseStudentEnrollment;
using WebApiLMS.Services;

namespace WebApiLMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseStudentEnrollmentController : ControllerBase
    {
        private readonly ICourseStudentEnrollmentService _service;
        public CourseStudentEnrollmentController(ICourseStudentEnrollmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseStudentEnrollmentRequest request)
        {
            var created = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseStudentEnrollmentRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}