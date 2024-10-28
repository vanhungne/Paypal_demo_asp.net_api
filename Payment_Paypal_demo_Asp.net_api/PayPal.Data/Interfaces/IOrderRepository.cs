using PayPal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Data.Interfaces
{
    public interface IOrderRepository : IRepository<Orders>
    {
        Task<Orders> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<Orders>> GetUserOrdersAsync(int userId);
        Task<Orders> GetOrderWithDetailsAsync(int orderId);
       
    }
}
