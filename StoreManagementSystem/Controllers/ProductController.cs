namespace StoreManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Data;

    public class ProductController : Controller
    {
        private readonly StoreDbContext dbContext;
        public ProductController(StoreDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            /*IEnumerable<Product> products = dbContext.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToList();*/

            return View();
        }
    }
}
