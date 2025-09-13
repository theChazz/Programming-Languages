using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.UserProgramEnrollments
{
    public class DetailsModel : PageModel
    {
        public int Id { get; set; } // Property to hold the course ID

        public void OnGet(int id)
        {
            Id = id; // Set the ID property when the page is accessed
        }
    }
}