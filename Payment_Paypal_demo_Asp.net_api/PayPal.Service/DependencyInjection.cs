using Microsoft.Extensions.DependencyInjection;
using PayPal.Service.Interfaces;
using PayPal.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IPaypalService, PaypalService>();
            return service;
        }
    }
}
