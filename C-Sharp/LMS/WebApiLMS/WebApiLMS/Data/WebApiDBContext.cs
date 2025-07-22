using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using WebApiLMS.Models;

namespace WebApiLMS.Data
{
    public class WebApiLMSDbContext : DbContext
    {
        public WebApiLMSDbContext(DbContextOptions<WebApiLMSDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; } // this will become the database name "Users"
        public DbSet<CourseModel> Courses { get; set; } // this will become the database name "Courses"
        public DbSet<ProgramModel> Programs { get; set; } // this will become the database name "Programs"
        public DbSet<ProgramCourseModel> ProgramCourses { get; set; }
        public DbSet<UserProgramEnrollmentModel> UserProgramEnrollments { get; set; } // New DbSet for UserProgramEnrollments
    }

}
