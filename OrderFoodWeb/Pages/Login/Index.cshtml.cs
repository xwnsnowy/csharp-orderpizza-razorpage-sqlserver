using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrderFoodWeb.Pages.Login
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Username == "admin" && Password == "password") 
            {
                // Authentication successful
                return RedirectToPage("/Index");
            }
            else
            {
                // Authentication failed
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
        }
    }
}
