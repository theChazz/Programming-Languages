using Microsoft.AspNetCore.Mvc;
using WebApiLMS.DTOs.CourseResource;
using WebApiLMS.Models;
using WebApiLMS.Services;

namespace WebApiLMS.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId}/resources")]
    public class CourseResourceController : ControllerBase
    {
        private readonly ICourseResourceService _service;

        public CourseResourceController(ICourseResourceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int courseId, [FromQuery] string? type, [FromQuery] string? module, [FromQuery] string? q)
        {
            try
            {
                var resources = await _service.GetAllAsync(courseId, type, module, q);
                var dtos = resources.Select(r => new CourseResourceDto
                {
                    Id = r.Id,
                    CourseId = r.CourseId,
                    Type = r.Type.ToString(),
                    Title = r.Title,
                    Description = r.Description,
                    Url = r.Url,
                    Provider = r.Provider,
                    MimeType = r.MimeType,
                    SizeBytes = r.SizeBytes,
                    StartsAt = r.StartsAt,
                    EndsAt = r.EndsAt,
                    Timezone = r.Timezone,
                    IsPublished = r.IsPublished,
                    Module = r.Module,
                    SortOrder = r.SortOrder,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                });
                return Ok(dtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while fetching resources");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int courseId, int id)
        {
            try
            {
                var resource = await _service.GetByIdAsync(id);
                if (resource == null || resource.CourseId != courseId)
                {
                    return NotFound();
                }

                var dto = new CourseResourceDto
                {
                    Id = resource.Id,
                    CourseId = resource.CourseId,
                    Type = resource.Type.ToString(),
                    Title = resource.Title,
                    Description = resource.Description,
                    Url = resource.Url,
                    Provider = resource.Provider,
                    MimeType = resource.MimeType,
                    SizeBytes = resource.SizeBytes,
                    StartsAt = resource.StartsAt,
                    EndsAt = resource.EndsAt,
                    Timezone = resource.Timezone,
                    IsPublished = resource.IsPublished,
                    Module = resource.Module,
                    SortOrder = resource.SortOrder,
                    CreatedAt = resource.CreatedAt,
                    UpdatedAt = resource.UpdatedAt
                };
                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while fetching the resource");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(int courseId, [FromBody] CreateCourseResourceRequest request)
        {
            try
            {
                if (courseId != request.CourseId)
                {
                    return BadRequest("Route courseId and body CourseId must match");
                }

                var created = await _service.CreateAsync(request);
                var dto = new CourseResourceDto
                {
                    Id = created.Id,
                    CourseId = created.CourseId,
                    Type = created.Type.ToString(),
                    Title = created.Title,
                    Description = created.Description,
                    Url = created.Url,
                    Provider = created.Provider,
                    MimeType = created.MimeType,
                    SizeBytes = created.SizeBytes,
                    StartsAt = created.StartsAt,
                    EndsAt = created.EndsAt,
                    Timezone = created.Timezone,
                    IsPublished = created.IsPublished,
                    Module = created.Module,
                    SortOrder = created.SortOrder,
                    CreatedAt = created.CreatedAt,
                    UpdatedAt = created.UpdatedAt
                };
                return CreatedAtAction(nameof(GetById), new { courseId = dto.CourseId, id = dto.Id }, dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest($"Invalid type. Allowed values: Document, Video, LiveSession. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the resource: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int courseId, int id, [FromBody] UpdateCourseResourceRequest request)
        {
            try
            {
                var existing = await _service.GetByIdAsync(id);
                if (existing == null || existing.CourseId != courseId)
                {
                    return NotFound();
                }

                var success = await _service.UpdateAsync(id, request);
                if (!success) return NotFound();

                return Ok(new { Message = "Resource updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the resource: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int courseId, int id)
        {
            try
            {
                var existing = await _service.GetByIdAsync(id);
                if (existing == null || existing.CourseId != courseId)
                {
                    return NotFound();
                }

                var success = await _service.DeleteAsync(id);
                if (!success) return NotFound();

                return Ok(new { Message = "Resource deleted successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the resource");
            }
        }
    }
}
