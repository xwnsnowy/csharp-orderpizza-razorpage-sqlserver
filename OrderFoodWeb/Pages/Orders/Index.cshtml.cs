using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderFoodWeb.Extensions;
using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderFoodWeb.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IEnumerable<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            Orders = await _orderService.GetAllOrdersByUserIdAsync(user.UserId);
        }
    }
}
