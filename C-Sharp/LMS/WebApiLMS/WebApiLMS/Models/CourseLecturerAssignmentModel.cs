using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiLMS.Models
{
    public class CourseLecturerAssignmentModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public CourseModel Course { get; set; }

        [Required]
        public int LecturerId { get; set; } // Same as UserId, but must have Role = "Lecturer"
        [ForeignKey("LecturerId")]
        public Users Lecturer { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.Now;
    }
}