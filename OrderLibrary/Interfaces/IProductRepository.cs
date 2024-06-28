using OrderLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderLibrary.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product> GetProductByNameAsync(string productName);
        Task<IEnumerable<Product>> GetLatestProductsAsync(int count);
    }
}
