using Microsoft.EntityFrameworkCore;
using WebApiLMS.Data;
using WebApiLMS.Models;

namespace WebApiLMS.Services
{
    public interface IProgramService
    {
        Task<List<ProgramModel>> GetAllProgramsAsync();
        Task<ProgramModel> GetProgramByIdAsync(int id);
        Task<ProgramModel> CreateProgramAsync(ProgramModel program);
        Task<bool> UpdateProgramAsync(int id, ProgramModel program);
        Task<bool> DeleteProgramAsync(int id);
    }

    public class ProgramService : IProgramService
    {
        private readonly WebApiLMSDbContext _context;

        public ProgramService(WebApiLMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramModel>> GetAllProgramsAsync()
        {
            return await _context.Programs.ToListAsync();
        }

        public async Task<ProgramModel> GetProgramByIdAsync(int id)
        {
            return await _context.Programs.FindAsync(id);
        }

        public async Task<ProgramModel> CreateProgramAsync(ProgramModel program)
        {
            _context.Programs.Add(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task<bool> UpdateProgramAsync(int id, ProgramModel program)
        {
            var existingProgram = await _context.Programs.FindAsync(id);
            if (existingProgram == null)
                return false;

            existingProgram.Name = program.Name;
            existingProgram.Code = program.Code;
            existingProgram.Level = program.Level;
            existingProgram.Department = program.Department;
            existingProgram.Status = program.Status;
            existingProgram.Description = program.Description;
            existingProgram.Type = program.Type;
            existingProgram.DurationMonths = program.DurationMonths;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProgramAsync(int id)
        {
            var program = await _context.Programs.FindAsync(id);
            if (program == null)
                return false;

            _context.Programs.Remove(program);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 