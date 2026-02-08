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

        public async Task<ProductAddInputModel> GetEmptyProductInputModelAsync()
        {
            ProductAddInputModel emptyProductModel = new ProductAddInputModel()
            {
                Categories = await GetAllCategoriesAsync(),
                Suppliers = await GetAllSuppliersAsync()
            };

            return emptyProductModel;
        }

        public async Task CreateProductAsync(ProductAddInputModel inputModel)
        {
            Product productToCreate = new Product()
            {
                Name = inputModel.Name,
                Price = inputModel.Price,
                Quantity = inputModel.Quantity,
                CategoryId = inputModel.CategoryId,
                SupplierId = inputModel.SupplierId
            };

            await dbContext.Products.AddAsync(productToCreate);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ProductAddInputModel?> GetProductInputModelByProductIdAsync(int productId)
        {
            Product? productToEdit = await dbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .SingleOrDefaultAsync(p => p.ProductId == productId);

            if (productToEdit == null)
            {
                return null;
            }

            ProductAddInputModel productToEditInputModel = new ProductAddInputModel()
            {
                Name = productToEdit.Name,
                Price = productToEdit.Price,
                Quantity = productToEdit.Quantity,
                CategoryId = productToEdit.CategoryId,
                SupplierId = productToEdit.SupplierId,
                Categories = await GetAllCategoriesAsync(),
                Suppliers = await GetAllSuppliersAsync()
            };

            return productToEditInputModel;
        }
        public async Task EditProductAsync(ProductAddInputModel inputModel, int productId)
        {
            Product? productToEdit = await dbContext
                .Products
                .SingleOrDefaultAsync(p => p.ProductId == productId);

            if (productToEdit == null)
            {
                throw new Exception($"Product with ID {productId} not found.");
            }

            productToEdit.Name = inputModel.Name;
            productToEdit.Price = inputModel.Price;
            productToEdit.Quantity = inputModel.Quantity;
            productToEdit.CategoryId = inputModel.CategoryId;
            productToEdit.SupplierId = inputModel.SupplierId;

            dbContext.Products.Update(productToEdit);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductAddCategoryViewModel>> GetAllCategoriesAsync()
        {
            IEnumerable<ProductAddCategoryViewModel> allCategories = await dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new ProductAddCategoryViewModel()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<IEnumerable<ProductAddSupplierViewModel>> GetAllSuppliersAsync()
        {
            IEnumerable<ProductAddSupplierViewModel> allSuppliers = await dbContext
                .Suppliers
                .AsNoTracking()
                .Select(s => new ProductAddSupplierViewModel()
                {
                    SupplierId = s.SupplierId,
                    SupplierName = s.SupplierName
                })
                .ToArrayAsync();

            return allSuppliers;
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await dbContext
                .Categories
                .AnyAsync(c => c.CategoryId == categoryId);
        }

        public async Task<bool> SupplierExistsAsync(int supplierId)
        {
            return await dbContext
                .Suppliers
                .AnyAsync(s => s.SupplierId == supplierId);
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await dbContext
                .Products
                .AnyAsync(p => p.ProductId == productId);
        }
    }
}
