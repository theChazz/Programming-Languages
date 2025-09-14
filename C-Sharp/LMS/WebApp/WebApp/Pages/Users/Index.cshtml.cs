using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace WebApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            // Set API configuration for JavaScript
            ViewData["ApiBaseUrl"] = _configuration["ApiSettings:BaseUrl"];
        }
    }
} 