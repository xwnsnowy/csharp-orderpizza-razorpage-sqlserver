using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using OrderLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }
        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _userRepository.GetUserByUserNameAsync(username);
        }
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            // Retrieve user by username
            var user = await _userRepository.GetUserByUserNameAsync(username);

            // Check if user exists and if password matches
            if (user != null && VerifyPassword(user.PasswordHash, password))
            {
                return true;
            }

            return false;
        }
        private bool VerifyPassword(string storedHash, string password)
        {
            // Implement your password hashing comparison logic here
            return storedHash == password; // Example: plain text comparison (do not use in production)
        }
    }
}
