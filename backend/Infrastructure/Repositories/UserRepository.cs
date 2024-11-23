using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Data;
using Core.Models;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OrdersDbContext _context;

        public UserRepository(OrdersDbContext context)
        {
            _context = context;
        }
        

        public async Task<User?> GetUser(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
