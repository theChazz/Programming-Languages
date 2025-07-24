namespace WebApiLMS.DTOs.CourseLecturerAssignment
{
    public class CreateCourseLecturerAssignmentRequest
    {
        public int CourseId { get; set; }
        public int LecturerId { get; set; }
    }
}