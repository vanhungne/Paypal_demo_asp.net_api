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
    public class OrderRepository : Repository<Orders>, IOrderRepository
    {
        private readonly DataDbContext _context;
        public OrderRepository(DataDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Orders> GetByOrderNumberAsync(string orderNumber)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<Orders> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShippingAddress)
                .Include(o => o.BillingAddress)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == orderId);
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
