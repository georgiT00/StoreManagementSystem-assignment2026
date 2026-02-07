namespace StoreManagementSystem.Services.Core
{
    using Interfaces;
    using Data;
    using ViewModels.Product;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly StoreDbContext dbContext;

        public ProductService(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductMinViewModel>> GetAllProductsAsync()
        {
            IEnumerable<ProductMinViewModel> productsToShow = await dbContext
                .Products
                .AsNoTracking()
                .Select(p => new ProductMinViewModel()
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name,
                    Price = p.Price.ToString("F2")
                })
                .ToArrayAsync();
            return productsToShow;
        }
    }
}
