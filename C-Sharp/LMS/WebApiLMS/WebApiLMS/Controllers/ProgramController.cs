using Microsoft.AspNetCore.Mvc;
using WebApiLMS.Models;
using WebApiLMS.Services;

namespace WebApiLMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrograms()
        {
            try
            {
                var programs = await _programService.GetAllProgramsAsync();
                return Ok(programs);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while fetching programs");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgram(int id)
        {
            try
            {
                var program = await _programService.GetProgramByIdAsync(id);
                if (program == null)
                {
                    return NotFound("Program not found");
                }
                return Ok(program);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while fetching the program");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramModel request)
        {
            try
            {
                var program = await _programService.CreateProgramAsync(request);
                return Ok(new { Id = program.Id, Message = "Program created successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the program");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] ProgramModel request)
        {
            try
            {
                var success = await _programService.UpdateProgramAsync(id, request);
                if (!success)
                {
                    return NotFound("Program not found");
                }
                return Ok(new { Message = "Program updated successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the program");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            try
            {
                var success = await _programService.DeleteProgramAsync(id);
                if (!success)
                {
                    return NotFound("Program not found");
                }
                return Ok(new { Message = "Program deleted successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the program");
            }
        }
    }
} 