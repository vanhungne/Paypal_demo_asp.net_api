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
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        private readonly DataDbContext _context;
        public ProductRepository(DataDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Products>> GetActiveProductsAsync()
        {
            return await _context.Products
                    .Where(p => p.IsActive && p.StockQuantity > 0)
                    .ToListAsync();
        }

        public async Task<Products> GetBySkuAsync(string sku)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.SKU == sku);
        }

        public async Task UpdateStockAsync(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.StockQuantity += quantity;
                if (product.StockQuantity < 0)
                {
                    throw new InvalidOperationException("Insufficient stock");
                }
                product.ModifiedDate = DateTime.UtcNow;
            }
        }
    }
}
