    using OrderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<bool> AuthenticateAsync(string username, string password);
        Task<User> GetUserByUserNameAsync(string username);
    }
}
