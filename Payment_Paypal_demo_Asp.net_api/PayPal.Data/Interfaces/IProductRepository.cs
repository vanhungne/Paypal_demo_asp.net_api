using PayPal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Data.Interfaces
{
    public interface IProductRepository : IRepository<Products>
    {
        Task<Products> GetBySkuAsync(string sku);
        Task<IEnumerable<Products>> GetActiveProductsAsync();
        Task UpdateStockAsync(int productId, int quantity);
    }
}
