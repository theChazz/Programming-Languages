using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLMS.Data;
using WebApiLMS.DTOs.CourseStudentEnrollment;
using WebApiLMS.Models;

namespace WebApiLMS.Services
{
    public class CourseStudentEnrollmentService : ICourseStudentEnrollmentService
    {
        private readonly WebApiLMSDbContext _context;
        public CourseStudentEnrollmentService(WebApiLMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseStudentEnrollmentDto>> GetAllAsync()
        {
            return await _context.CourseStudentEnrollments
                .Select(e => new CourseStudentEnrollmentDto
                {
                    Id = e.Id,
                    CourseId = e.CourseId,
                    StudentId = e.StudentId,
                    EnrolledAt = e.EnrolledAt,
                    Progress = e.Progress
                }).ToListAsync();
        }

        public async Task<CourseStudentEnrollmentDto> GetByIdAsync(int id)
        {
            var e = await _context.CourseStudentEnrollments.FindAsync(id);
            if (e == null) return null;
            return new CourseStudentEnrollmentDto
            {
                Id = e.Id,
                CourseId = e.CourseId,
                StudentId = e.StudentId,
                EnrolledAt = e.EnrolledAt,
                Progress = e.Progress
            };
        }

        public async Task<CourseStudentEnrollmentDto> CreateAsync(CreateCourseStudentEnrollmentRequest request)
        {
            var entity = new CourseStudentEnrollmentModel
            {
                CourseId = request.CourseId,
                StudentId = request.StudentId,
                Progress = request.Progress
            };
            _context.CourseStudentEnrollments.Add(entity);
            await _context.SaveChangesAsync();
            return new CourseStudentEnrollmentDto
            {
                Id = entity.Id,
                CourseId = entity.CourseId,
                StudentId = entity.StudentId,
                EnrolledAt = entity.EnrolledAt,
                Progress = entity.Progress
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateCourseStudentEnrollmentRequest request)
        {
            var entity = await _context.CourseStudentEnrollments.FindAsync(id);
            if (entity == null) return false;
            entity.Progress = request.Progress;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CourseStudentEnrollments.FindAsync(id);
            if (entity == null) return false;
            _context.CourseStudentEnrollments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}