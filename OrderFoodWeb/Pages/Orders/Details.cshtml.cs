using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderFoodWeb.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public DetailsModel(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        public Order Order { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Order = await _orderService.GetOrderByIdAsync(id);

            if (Order == null)
            {
                return NotFound();
            }

            OrderDetails = await _orderDetailService.GetDetailsByOrderIdAsync(id);

            if (OrderDetails == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
