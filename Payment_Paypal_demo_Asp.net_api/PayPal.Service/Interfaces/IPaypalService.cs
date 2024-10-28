using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Service.Interfaces
{
    public interface IPaypalService
    {
        Task<string> CreatePaymentAsync(decimal amount, string orderNumber);
        Task<(string Status, string OrderId)> CapturePaymentAsync(string token);
    }
}
