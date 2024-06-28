using OrderLibrary.Interfaces;
using OrderLibrary.Models;

namespace OrderLibrary.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(OrderFoodDBContext context) : base(context)
        {
        }

    }
}
