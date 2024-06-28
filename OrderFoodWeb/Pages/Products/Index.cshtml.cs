using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderLibrary.Models;
using OrderLibrary.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderFoodWeb.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Product> Products { get; set; }
        public int? CategoryId { get; set; }

        public async Task OnGetAsync(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                Products = (await _productService.GetProductsByCategoryAsync(categoryId.Value)).ToList();
            }
            else
            {
                Products = (await _productService.GetAllProductsAsync()).ToList();
            }
        }
    }
}
