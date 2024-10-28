using Microsoft.Extensions.DependencyInjection;
using PayPal.Data.Interfaces;
using PayPal.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddScoped<IOrderRepository,OrderRepository>();
            service.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            service.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            service.AddScoped<IPaymentRepository, PaymentRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            return service;
        }
    }
}
