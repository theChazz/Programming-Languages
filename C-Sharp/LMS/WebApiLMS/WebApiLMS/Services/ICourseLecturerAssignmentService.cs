using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLMS.DTOs.CourseLecturerAssignment;

namespace WebApiLMS.Services
{
    public interface ICourseLecturerAssignmentService
    {
        Task<IEnumerable<CourseLecturerAssignmentDto>> GetAllAsync();
        Task<CourseLecturerAssignmentDto> GetByIdAsync(int id);
        Task<CourseLecturerAssignmentDto> CreateAsync(CreateCourseLecturerAssignmentRequest request);
        Task<bool> UpdateAsync(int id, UpdateCourseLecturerAssignmentRequest request);
        Task<bool> DeleteAsync(int id);
    }
}