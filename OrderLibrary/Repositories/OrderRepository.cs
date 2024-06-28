using OrderLibrary.Models;

namespace OrderLibrary.Repositories
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(OrderFoodDBContext context) : base(context)
        {
        }

        // Add any specific methods related to Product if necessary
    }
}
