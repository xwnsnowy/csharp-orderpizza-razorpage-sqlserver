using OrderLibrary.Models;

namespace OrderLibrary.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(OrderFoodDBContext context) : base(context)
        {
        }

        // Add any specific methods related to User if necessary
    }
}
