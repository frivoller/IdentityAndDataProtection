using Microsoft.AspNetCore.Identity;
using YourNamespace.Data;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public void CreateUser(string email, string password)
        {
            var user = new User { Email = email };
            user.Password = _passwordHasher.HashPassword(user, password);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
} 