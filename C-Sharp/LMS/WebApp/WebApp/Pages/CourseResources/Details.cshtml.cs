using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.CourseResources
{
    public class DetailsModel : PageModel
    {
        public int CourseId { get; set; }
        public int Id { get; set; }
        public void OnGet(int courseId)
        {
            CourseId = courseId;
            if (Request.Query.ContainsKey("id") && int.TryParse(Request.Query["id"], out var resourceId))
            {
                Id = resourceId;
            }
        }
    }
}
