using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.CourseLecturerAssignments
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