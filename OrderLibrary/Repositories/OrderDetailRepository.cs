using Microsoft.EntityFrameworkCore;
using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Repositories
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(OrderFoodDBContext context) : base(context)
        {
        }
        public async Task<IEnumerable<OrderDetail>> GetDetailsByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                                 .Where(detail => detail.OrderId == orderId)
                                 .ToListAsync();
        }
    }
}
