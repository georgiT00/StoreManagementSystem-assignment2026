namespace StoreManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> Index(int? categoryId)
        {
            IEnumerable<ProductMinViewModel> products = categoryId.HasValue
               ? await productService.GetProductsByCategoryIdAsync(categoryId.Value)
               : await productService.GetAllProductsAsync();


            IEnumerable<ProductCategoryViewModel> categories = await productService
                .GetAllCategoriesAsync();

            ProductIndexViewModel productIndexViewModel = new ProductIndexViewModel()
            {
                Products = products,
                Categories = categories
            };

            return View(productIndexViewModel);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            ProductDetailsViewModel productDetails = await productService
                .GetProductDetailsByIdAsync(id);

            if (productDetails == null)
            {
                return NotFound();
            }

            return View(productDetails);
        }
    }
}
