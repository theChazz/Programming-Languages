using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.CourseStudentEnrollments
{
    public class EditModel : PageModel
    {
        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
        }
    }
} 