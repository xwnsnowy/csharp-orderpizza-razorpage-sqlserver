using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderLibrary.Models;
using OrderLibrary.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 8;

        public async Task OnGetAsync(int? categoryId, int? pageIndex)
        {
            Categories = (await _categoryService.GetAllCategoriesAsync()).ToList();
            CategoryId = categoryId;
            CurrentPage = pageIndex ?? 1;

            if (categoryId.HasValue)
            {
                var productsResult = await _productService.GetProductsByCategoryAsync(categoryId.Value);
                Products = productsResult.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                TotalPages = (int)Math.Ceiling(productsResult.Count() / (double)PageSize);
            }
            else
            {
                var productsResult = await _productService.GetAllProductsAsync();
                Products = productsResult.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                TotalPages = (int)Math.Ceiling(productsResult.Count() / (double)PageSize);
            }
        }
    }
}
