namespace StoreManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Core.Interfaces;
    using ViewModels.Product;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductMinViewModel> products = await productService
                .GetAllProductsAsync();

            return View(products);
        }
    }
}
