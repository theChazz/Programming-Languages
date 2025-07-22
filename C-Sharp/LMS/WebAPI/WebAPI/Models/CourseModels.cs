using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CourseRequest
    {
        [Required]
        [StringLength(255)]
        public string CourseName { get; set; }

        public string Description { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [StringLength(20)]
        public string Difficulty { get; set; }

        public string Syllabus { get; set; }

        public string Prerequisites { get; set; }
    }

    public class CourseResponse
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public string Syllabus { get; set; }
        public string Prerequisites { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 