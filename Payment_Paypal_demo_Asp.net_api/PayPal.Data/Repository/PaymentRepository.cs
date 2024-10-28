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
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly DataDbContext _context;
        public PaymentRepository(DataDbContext context) : base(context)
        {
            _context=context;
        }

        public async Task<Payment> GetByPaypalOrderIdAsync(string paypalOrderId)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.PaypalOrderId == paypalOrderId);
        }
        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .Where(p => p.Order.UserId == userId)
                .ToListAsync();
        }
    }
}
