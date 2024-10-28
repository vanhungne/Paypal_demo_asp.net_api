using PayPal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Data.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetByPaypalOrderIdAsync(string paypalOrderId);
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId);
        Task AddAsync(Payment payment);
    }
}
