namespace StoreManagementSystem.Services.Core
{
    using Interfaces;
    using Data;
    using ViewModels.Product;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using StoreManagementSystem.Data.Models;

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
                .Select(p => new ProductMinViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name,
                    Price = p.Price.ToString("F2")
                })
                .ToArrayAsync();

            return productsToShow;
        }

        public async Task<ProductDetailsViewModel?> GetProductDetailsByIdAsync(int productId)
        {
            Product? checkProduct = await dbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.ProductId == productId);

            if (checkProduct == null)
            {
                return null;
            }

            ProductDetailsViewModel productDetailsViewModel = new ProductDetailsViewModel()
            {
                ProductId = checkProduct.ProductId,
                ProductName = checkProduct.Name,
                Price = checkProduct.Price.ToString("F2"),
                Quantity = checkProduct.Quantity,
                CategoryName = checkProduct.Category.CategoryName,
                SupplierName = checkProduct.Supplier != null ? checkProduct.Supplier.SupplierName : "N/A"
            };

            return productDetailsViewModel;
        }

        public async Task<IEnumerable<ProductMinViewModel>?> GetProductsByCategoryIdAsync(int categoryId)
        { 
            bool categoryExists = await CategoryExistsAsync(categoryId);

            if (!categoryExists)
            {
                return null;
            }

            IEnumerable<ProductMinViewModel> productsByCategory = await dbContext
                .Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryId == categoryId)
                .AsNoTracking()
                .Select(p => new ProductMinViewModel()
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name,
                    Price = p.Price.ToString("F2")
                })
                .ToArrayAsync();

            return productsByCategory;
        }

        public async Task<IEnumerable<ProductCategoryViewModel>> GetAllCategoriesAsync()
        {
            IEnumerable<ProductCategoryViewModel> allCategories = await dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new ProductCategoryViewModel()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await dbContext
                .Categories
                .AnyAsync(c => c.CategoryId == categoryId);
        }
    }
}
