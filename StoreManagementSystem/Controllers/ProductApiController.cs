namespace StoreManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Interfaces;
    using ViewModels.Product;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductApiController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductDetails(int id)
        {
            ProductDetailsViewModel? viewModel = await productService.GetProductDetailsByIdAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return Ok(viewModel);
        }

    }
}
