using PayPal.Data.Entities;
using PayPal.Data.Enum;
using PayPal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto, int userId);
        Task<OrderDto> GetOrderByNumberAsync(string orderNumber);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId);
        Task UpdateOrderStatusAsync(string orderNumber, OrderStatus status);
        Task ProcessPaymentAsync(string orderNumber, string paypalToken);
    }
}
