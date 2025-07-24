using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLMS.DTOs.CourseStudentEnrollment;

namespace WebApiLMS.Services
{
    public interface ICourseStudentEnrollmentService
    {
        Task<IEnumerable<CourseStudentEnrollmentDto>> GetAllAsync();
        Task<CourseStudentEnrollmentDto> GetByIdAsync(int id);
        Task<CourseStudentEnrollmentDto> CreateAsync(CreateCourseStudentEnrollmentRequest request);
        Task<bool> UpdateAsync(int id, UpdateCourseStudentEnrollmentRequest request);
        Task<bool> DeleteAsync(int id);
    }
}