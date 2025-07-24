namespace WebApiLMS.DTOs.CourseLecturerAssignment
{
    public class CourseLecturerAssignmentDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int LecturerId { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}