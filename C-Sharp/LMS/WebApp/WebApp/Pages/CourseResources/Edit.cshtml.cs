using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.CourseResources
{
    public class EditModel : PageModel
    {
        public int CourseId { get; set; }
        public void OnGet(int courseId)
        {
            CourseId = courseId;
        }
    }
}
