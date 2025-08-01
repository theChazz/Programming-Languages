using Microsoft.EntityFrameworkCore;
using WebApiLMS.Data;
using WebApiLMS.Models;

namespace WebApiLMS.Services
{
    public interface ICourseService
    {
        Task<List<CourseModel>> GetAllCoursesAsync();
        Task<CourseModel> GetCourseByIdAsync(int id);
        Task<CourseModel> CreateCourseAsync(CourseModel course);
        Task<bool> UpdateCourseAsync(int id, CourseModel course);
        Task<bool> DeleteCourseAsync(int id);
    }

    public class CourseService : ICourseService
    {
        private readonly WebApiLMSDbContext _context;

        public CourseService(WebApiLMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseModel>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<CourseModel> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<CourseModel> CreateCourseAsync(CourseModel course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> UpdateCourseAsync(int id, CourseModel course)
        {
            var existingCourse = await _context.Courses.FindAsync(id);
            if (existingCourse == null)
                return false;

            existingCourse.CourseName = course.CourseName;
            existingCourse.Description = course.Description;
            existingCourse.Category = course.Category;
            existingCourse.Difficulty = course.Difficulty;
            existingCourse.Syllabus = course.Syllabus;
            existingCourse.Prerequisites = course.Prerequisites;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 