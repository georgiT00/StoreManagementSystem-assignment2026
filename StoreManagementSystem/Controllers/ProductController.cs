namespace StoreManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Core.Interfaces;
    using ViewModels.Product;

    using static GCommon.OutputMessages.Product;
    using static GCommon.AppConstants;


    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        private readonly ICartService cartService;

        public ProductController(IProductService productService, ICartService cartService)
        {
            this.productService = productService;
            this.cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId)
        {
            IEnumerable<ProductMinViewModel> products = categoryId.HasValue
               ? await productService.GetProductsByCategoryIdAsync(categoryId.Value)
               : await productService.GetAllProductsAsync();


            IEnumerable<ProductAddCategoryViewModel> categories = await productService
                .GetAllCategoriesAsync();

            ProductIndexViewModel productIndexViewModel = new ProductIndexViewModel()
            {
                Products = products,
                Categories = categories
            };

            return View(productIndexViewModel);
        }


        [HttpGet]
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
        public async Task<IActionResult> Create()
        {
            ProductAddInputModel productAddViewModel = await productService
                .GetEmptyProductInputModelAsync();

            return View(productAddViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductAddInputModel inputModel)
        {
            inputModel.Categories = await productService
                .GetAllCategoriesAsync();
            inputModel.Suppliers = await productService
                .GetAllSuppliersAsync();

            bool isCategoryValid = await productService
                .CategoryExistsAsync(inputModel.CategoryId);

            bool isSupplierValid = await productService
                .SupplierExistsAsync(inputModel.SupplierId ?? 0);

            if (!isCategoryValid)
            {
                ModelState.AddModelError(nameof(inputModel.CategoryId), ProductCategoryNotFoundMsg);
            }

            if (!isSupplierValid && inputModel.SupplierId.HasValue)
            {
                ModelState.AddModelError(nameof(inputModel.SupplierId), ProductSupplierNotFoundMsg);
            }

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            await productService.CreateProductAsync(inputModel);
            TempData[SuccessTempDataKey] = String.Format(ProductAddedMsg, inputModel.Name);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductAddInputModel productEditViewModel = await productService
                .GetProductInputModelByProductIdAsync(id);

            if (productEditViewModel == null)
            {
                return NotFound();
            }

            return View(productEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, ProductAddInputModel inputModel)
        {
            inputModel.Categories = await productService
                .GetAllCategoriesAsync();

            inputModel.Suppliers = await productService
                .GetAllSuppliersAsync();

            bool isCategoryValid = await productService
                .CategoryExistsAsync(inputModel.CategoryId);

            bool isSupplierValid = await productService
                .SupplierExistsAsync(inputModel.SupplierId ?? 0);

            bool isProductValid = await productService
                .ProductExistsAsync(id);

            if (!isProductValid)
            {
                return NotFound();
            }

            if (!isCategoryValid)
            {
                ModelState.AddModelError(nameof(inputModel.CategoryId), ProductCategoryNotFoundMsg);
            }

            if (!isSupplierValid && inputModel.SupplierId.HasValue)
            {
                ModelState.AddModelError(nameof(inputModel.SupplierId), ProductSupplierNotFoundMsg);
            }

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            await productService.EditProductAsync(inputModel, id);
            TempData[SuccessTempDataKey] = String.Format(ProductEditedMsg, inputModel.Name);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProductDetailsViewModel productDetails = await productService
                .GetProductDetailsByIdAsync(id);

            if (productDetails == null)
            {
                return NotFound();
            }

            ProductDeleteViewModel viewModel = new ProductDeleteViewModel()
            {
                ProductName = productDetails.ProductName
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int id, ProductDeleteViewModel viewModel)
        {
            bool isProductValid = await productService
                .ProductExistsAsync(id);

            if (!isProductValid)
            {
                return NotFound();
            }

            ProductDetailsViewModel? productDetails = await productService
                .GetProductDetailsByIdAsync(id);

            try
            {
                await productService.DeleteProductAsync(id);
            }

            catch(Exception exception)
            {
                TempData[ErrorTempDataKey] = exception.Message;
                return View(viewModel);
            }

            TempData[SuccessTempDataKey] = String.Format(ProductDeletedMsg, productDetails!.ProductName);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromRoute]int id)
        {
            string userId = GetUserId();

            bool isProductValid = await productService
                .ProductExistsAsync(id);

            if (!isProductValid)
            {
                return NotFound();
            }

            try
            {
                await cartService.AddToCartAsync(userId, id);
            }

            catch (Exception exception)
            {
                TempData[ErrorTempDataKey] = exception.Message;
                return RedirectToAction(nameof(Index));
            }


            ProductDetailsViewModel product = await productService
            .GetProductDetailsByIdAsync(id);

            TempData[SuccessTempDataKey] = String.Format(ProductAddedToCartMsg, product!.ProductName);

            return RedirectToAction(nameof(Index));
        }
    }
}
