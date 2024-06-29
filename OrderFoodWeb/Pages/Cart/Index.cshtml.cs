using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderFoodWeb.Pages.Cart
{
    public class IndexModel : PageModel
    {
        public List<dynamic> CartProducts { get; set; }
        public double Subtotal { get; set; }
        public double DeliveryFee { get; set; }
        public double Total { get; set; }
        public AddressModel Address { get; set; } = new AddressModel();

        public IActionResult OnGet()
        {
            // Retrieve cart items from session
            CartProducts = HttpContext.Session.Get<List<dynamic>>("Cart") ?? new List<dynamic>();

            // Calculate subtotal, delivery fee, and total
            CalculateCartTotal();

            return Page();
        }

        public IActionResult OnPostProceedToCheckout()
        {
            // Validate and process checkout (dummy implementation)
            if (ModelState.IsValid)
            {
                // Redirect to checkout page or payment gateway
                return RedirectToPage("/Checkout/Index");
            }
            else
            {
                // Return to cart page if validation fails
                return Page();
            }
        }

        public IActionResult OnPostRemoveFromCart(int productId)
        {
            // Retrieve current cart items from session
            CartProducts = HttpContext.Session.Get<List<dynamic>>("Cart") ?? new List<dynamic>();

            // Remove the product from cart
            CartProducts.RemoveAll(p => p.Id == productId);

            // Update session with the updated cart items
            HttpContext.Session.Set("Cart", CartProducts);

            // Recalculate total
            CalculateCartTotal();

            return RedirectToPage("/Cart/Index");
        }

        private void CalculateCartTotal()
        {
            Subtotal = CartProducts.Sum(p => (double)p.Price);
            DeliveryFee = 5.0; // Example delivery fee
            Total = Subtotal + DeliveryFee;
        }
    }

    public class AddressModel
    {
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
