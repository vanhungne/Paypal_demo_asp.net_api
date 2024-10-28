using Microsoft.EntityFrameworkCore;
using PayPal.Data.Data;
using PayPal.Data.Entities;
using PayPal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Data.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private readonly DataDbContext _context;
        public UserRepository(DataDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<Orders>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Payment)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
