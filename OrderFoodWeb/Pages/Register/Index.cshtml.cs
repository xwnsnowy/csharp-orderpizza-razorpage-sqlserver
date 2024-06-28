using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OrderFoodWeb.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [BindProperty]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

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
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please fill out all fields correctly.";
                return Page();
            }

            try
            {
                var user = new User
                {
                    UserName = Username,
                    Email = Email,
                    PasswordHash = HashPassword(Password)
                };

                await _userService.AddUserAsync(user);

                return RedirectToPage("/Login/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        private string HashPassword(string password)
        {
            // Replace this method with your actual password hashing implementation
            return password; // For illustration purpose only; do not store passwords in plain text in production
        }
    }
}
