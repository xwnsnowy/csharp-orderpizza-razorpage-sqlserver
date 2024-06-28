using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLibrary.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(OrderFoodDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetLatestProductsAsync(int count)
        {
            return await _context.Products
                                 .OrderByDescending(p => p.ProductId)
                                 .Take(count)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                                 .Where(p => p.CategoryId == categoryId)
                                 .ToListAsync();
        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductName == productName);
        }
    }
}
