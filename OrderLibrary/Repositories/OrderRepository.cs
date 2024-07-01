using OrderLibrary.Interfaces;
using OrderLibrary.Models;

namespace OrderLibrary.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderFoodDBContext context) : base(context)
        {
        }

    }
}
