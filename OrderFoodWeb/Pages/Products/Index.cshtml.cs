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
        private readonly ICategoryService _categoryService;

        public IndexModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IList<Product> Products { get; set; }
        public IList<Category> Categories { get; set; }
        public int? CategoryId { get; set; }

        public async Task OnGetAsync(int? categoryId)
        {
            Categories = (await _categoryService.GetAllCategoriesAsync()).ToList();

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
