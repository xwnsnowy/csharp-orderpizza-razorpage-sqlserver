using Microsoft.EntityFrameworkCore;
using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using System.Security.Cryptography.X509Certificates;

namespace OrderLibrary.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(OrderFoodDBContext context) : base(context)
        {

        }
        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
