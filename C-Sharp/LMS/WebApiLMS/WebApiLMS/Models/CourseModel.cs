using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiLMS.Models
{
    public class CourseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        public string CourseName { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Difficulty { get; set; }

        public string Syllabus { get; set; }

        public string Prerequisites { get; set; }

        [StringLength(2048)]
        [Url]
        public string PdfUrl { get; set; } = string.Empty;

        [StringLength(2048)]
        [Url]
        public string WordUrl { get; set; } = string.Empty;

        [StringLength(2048)]
        [Url]
        public string PowerPointUrl { get; set; } = string.Empty;

        [StringLength(2048)]
        [Url]
        public string ExcelUrl { get; set; } = string.Empty;

        [StringLength(2048)]
        [Url]
        public string ZipUrl { get; set; } = string.Empty;

        [StringLength(2048)]
        [Url]
        public string TeamsJoinUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 