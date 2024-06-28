using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrderFoodWeb.Pages.Register
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please fill out all fields correctly.";
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            // Add logic to save the new user to the database here
            // Example:
            // var user = new User { Username = Username, Email = Email, Password = PasswordHash(Password) };
            // _context.Users.Add(user);
            // _context.SaveChanges();

            // Redirect to login page after successful registration
            return RedirectToPage("/Login/Index");
        }
    }
}
