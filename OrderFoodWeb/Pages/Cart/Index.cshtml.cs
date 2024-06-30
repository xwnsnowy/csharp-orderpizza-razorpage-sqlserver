using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using OrderFoodWeb.Extensions;
using OrderFoodWeb.Hubs;
using System.Collections.Generic;
using System.Linq;

namespace OrderFoodWeb.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IHubContext<CartHub> _hubContext;
        public List<ProductData> CartProducts { get; set; } = new List<ProductData>();
        public double Subtotal { get; set; }
        public double DeliveryFee { get; set; } = 5.0;
        public double Total { get; set; }
        public AddressModel Address { get; set; } = new AddressModel();
        public IndexModel(IHubContext<CartHub> hubContext)
        {
            _hubContext = hubContext;
        }
        private int GetOrderCount()
        {
            return CartProducts?.Count ?? 0; 
        }

        public IActionResult OnGet()
        {
            // Retrieve cart items from session
            CartProducts = HttpContext.Session.GetObjectFromJson<List<ProductData>>("Cart") ?? new List<ProductData>();

            HttpContext.Session.SetInt32("CartCount", GetOrderCount());

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
            CartProducts = HttpContext.Session.GetObjectFromJson<List<ProductData>>("Cart") ?? new List<ProductData>();

            // Remove the product from cart
            CartProducts.RemoveAll(p => p.Id == productId);

            // Update session with the updated cart items
            HttpContext.Session.SetObjectAsJson("Cart", CartProducts);

            // Recalculate total
            CalculateCartTotal();

            return RedirectToPage("/Cart/Index");
        }

        public IActionResult OnPostAddToCart([FromBody] ProductData productData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Retrieve current cart items from session
            CartProducts = HttpContext.Session.GetObjectFromJson<List<ProductData>>("Cart") ?? new List<ProductData>();

            // Add the new product to the cart
            CartProducts.Add(productData);

            // Update session with the updated cart items
            HttpContext.Session.SetObjectAsJson("Cart", CartProducts);

            // Recalculate total
            CalculateCartTotal();

            _hubContext.Clients.All.SendAsync("ReceiveCartCount", CartProducts.Count);

            return new JsonResult(new { message = "Product added to cart successfully." });
        }

        private void CalculateCartTotal()
        {
            Subtotal = CartProducts.Sum(p => p.BasePrice + p.Extras.Sum(e => e.Price));
            Total = Subtotal + DeliveryFee;
        }
    }

    public class ProductData
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public List<ExtraData> Extras { get; set; } = new List<ExtraData>();
        public double BasePrice { get; set; }
        public double Price => BasePrice + Extras.Sum(e => e.Price);
    }

    public class ExtraData
    {
        public string Name { get; set; }
        public double Price { get; set; }
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
