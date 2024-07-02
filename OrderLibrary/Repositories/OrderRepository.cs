using Microsoft.EntityFrameworkCore;
using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using System.Threading.Tasks;

namespace OrderLibrary.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderFoodDBContext context) : base(context)
        {
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                                 .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetAllOrderByUserIdAsync(int userId)
        {
            return await _context.Orders
                                 .Where(order => order.UserId == userId)
                                 .ToListAsync();
        }
    }
}
