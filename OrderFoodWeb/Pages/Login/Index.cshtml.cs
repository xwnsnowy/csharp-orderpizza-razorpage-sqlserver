using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderFoodWeb.Extensions;
using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderFoodWeb.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Username and password are required.";
                return Page();
            }

            var isAuthenticated = await _userService.AuthenticateAsync(Username, Password);

            if (!isAuthenticated)
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            var user = await _userService.GetUserByUserNameAsync(Username);
            if (user == null)
            {
                ErrorMessage = "User not found.";
                return Page();
            }

            HttpContext.Session.SetString("Username", Username);
            HttpContext.Session.SetObjectAsJson("User", user);

            return RedirectToPage("/Index"); 
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("User");
            return RedirectToPage("/Login/Index");
        }
    }
}
