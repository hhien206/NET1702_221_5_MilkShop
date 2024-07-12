using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using MilkShopRazorWebApp.Models;

namespace MilkShopRazorWebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Username { get; set; }
        [BindProperty] public int Password { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Username.ToLower() == "admin" && Password == 123)
            {
                HttpContext.Session.SetString("Role", "1");
                return RedirectToPage("/Index");
            }
            else
            {
                ViewData["message"] = "Username or password invalid";
                return Page();
            }
        }
    }
}

