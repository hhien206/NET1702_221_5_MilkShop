using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MilkShopRazorWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if(HttpContext.Session.GetString("Role") == "1")
            {
                return Page();
            }
            TempData["Message"] = "You do not have permission to do this function!";
            return RedirectToPage("/Login");
        }
    }
}
