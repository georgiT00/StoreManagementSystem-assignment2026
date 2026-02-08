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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ProductAddInputModel productAddViewModel = await productService
                .GetEmptyProductInputModelAsync();

            return View(productAddViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProductAddInputModel inputModel)
        {
            inputModel.Categories = await productService.GetAllCategoriesAsync();
            inputModel.Suppliers = await productService.GetAllSuppliersAsync();

            bool isCategoryValid = await productService.CategoryExistsAsync(inputModel.CategoryId);

            bool isSupplierValid = await productService.SupplierExistsAsync(inputModel.SupplierId ?? 0);

            if (!isCategoryValid)
            {
                ModelState.AddModelError(nameof(inputModel.CategoryId), "Selected category does not exist.");
            }

            if (!isSupplierValid && inputModel.SupplierId.HasValue)
            {
                ModelState.AddModelError(nameof(inputModel.SupplierId), "Selected supplier does not exist.");
            }

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            await productService.CreateProductAsync(inputModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
