using OrderLibrary.Interfaces;
using OrderLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderFoodWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;

        public IndexModel(ILogger<IndexModel> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public List<Product> NewestProducts { get; set; }

        public async Task OnGetAsync()
        {
            NewestProducts = (await _productService.GetLatestProducts(4)).ToList();
        }
    }
}
