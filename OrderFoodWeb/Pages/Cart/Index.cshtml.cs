using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OrderFoodWeb.Extensions;
using OrderFoodWeb.Hubs;
using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using OrderLibrary.Repositories;
using OrderLibrary.Services;
using System.Collections.Generic;
using System.Linq;

namespace OrderFoodWeb.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderDetailRepository _orderDetailRepository;

        private readonly IHubContext<CartHub> _hubContext;
        public List<ProductData> CartProducts { get; set; } = new List<ProductData>();
        public double Subtotal { get; set; }
        public double DeliveryFee { get; set; } = 5.0;
        public double Total { get; set; }

        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public IndexModel(
            IHubContext<CartHub> hubContext,
            IOrderService orderService,
            IOrderDetailService orderDetailService,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _hubContext = hubContext;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        private int GetOrderCount()
        {
            return CartProducts.Count;
        }

        public IActionResult OnGet()
        {
            // Check if the user is logged in
            //if (HttpContext.Session.GetString("Username") == null)
            //{
            //    return RedirectToPage("/Account/Login");
            //}
            // Retrieve cart items from session
            CartProducts = HttpContext.Session.GetObjectFromJson<List<ProductData>>("Cart") ?? new List<ProductData>();

            HttpContext.Session.SetInt32("CartCount", GetOrderCount());

            // Calculate subtotal, delivery fee, and total
            CalculateCartTotal();

            HttpContext.Session.SetDouble("Subtotal", Subtotal);
            HttpContext.Session.SetDouble("Total", Total);

            return Page();
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
        public async Task<IActionResult> OnPostProceedToCheckout()
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            CartProducts = HttpContext.Session.GetObjectFromJson<List<ProductData>>("Cart") ?? new List<ProductData>();

            Subtotal = HttpContext.Session.GetDouble("Subtotal");
            Total = HttpContext.Session.GetDouble("Total");

            Phone = Request.Form["Phone"];
            StreetAddress = Request.Form["StreetAddress"];
            PostalCode = Request.Form["PostalCode"];
            City = Request.Form["City"];
            Country = Request.Form["Country"];

            //if (string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(StreetAddress) || string.IsNullOrEmpty(PostalCode) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(Country))
            //{
            //    ErrorMessage = "All fields are required.";
            //    return Page();
            //}

            if (ModelState.IsValid)
            {

                var order = new Order
                {
                    UserId = user.UserId,
                    Phone = Phone,
                    StreetAddress = StreetAddress,
                    PostalCode = PostalCode,
                    City = City,
                    Country = Country,
                    Subtotal = Convert.ToDecimal(Subtotal),
                    DeliveryFee = Convert.ToDecimal(DeliveryFee),
                    Total = Convert.ToDecimal(Total),
                    OrderDate = DateTime.Now
                };

                await _orderRepository.AddAsync(order);

                foreach (var cartProduct in CartProducts)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = cartProduct.Id,
                        ProductName = cartProduct.Name,
                        Size = cartProduct.Size,
                        BasePrice = (decimal)cartProduct.BasePrice,
                        Extras = string.Join(", ", cartProduct.Extras.Select(e => $"{e.Name} ${e.Price}")),
                        Quantity = 1,
                        Price = (decimal)cartProduct.Price
                    };
                    await _orderDetailRepository.AddAsync(orderDetail);
                }

                HttpContext.Session.Remove("Cart");

                return RedirectToPage("/Checkout/Success");
            }
            else
            {
                return Page();
            }
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
        public string? Name { get; set; }
        public double Price { get; set; }
    }
}
