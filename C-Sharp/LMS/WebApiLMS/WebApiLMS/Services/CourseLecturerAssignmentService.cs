using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLMS.Data;
using WebApiLMS.DTOs.CourseLecturerAssignment;
using WebApiLMS.Models;

namespace WebApiLMS.Services
{
    public class CourseLecturerAssignmentService : ICourseLecturerAssignmentService
    {
        private readonly WebApiLMSDbContext _context;
        public CourseLecturerAssignmentService(WebApiLMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseLecturerAssignmentDto>> GetAllAsync()
        {
            return await _context.CourseLecturerAssignments
                .Select(a => new CourseLecturerAssignmentDto
                {
                    Id = a.Id,
                    CourseId = a.CourseId,
                    LecturerId = a.LecturerId,
                    AssignedAt = a.AssignedAt
                }).ToListAsync();
        }

        public async Task<CourseLecturerAssignmentDto> GetByIdAsync(int id)
        {
            var a = await _context.CourseLecturerAssignments.FindAsync(id);
            if (a == null) return null;
            return new CourseLecturerAssignmentDto
            {
                Id = a.Id,
                CourseId = a.CourseId,
                LecturerId = a.LecturerId,
                AssignedAt = a.AssignedAt
            };
        }

        public async Task<CourseLecturerAssignmentDto> CreateAsync(CreateCourseLecturerAssignmentRequest request)
        {
            var entity = new CourseLecturerAssignmentModel
            {
                CourseId = request.CourseId,
                LecturerId = request.LecturerId
            };
            _context.CourseLecturerAssignments.Add(entity);
            await _context.SaveChangesAsync();
            return new CourseLecturerAssignmentDto
            {
                Id = entity.Id,
                CourseId = entity.CourseId,
                LecturerId = entity.LecturerId,
                AssignedAt = entity.AssignedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateCourseLecturerAssignmentRequest request)
        {
            // No updatable fields for now
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CourseLecturerAssignments.FindAsync(id);
            if (entity == null) return false;
            _context.CourseLecturerAssignments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}