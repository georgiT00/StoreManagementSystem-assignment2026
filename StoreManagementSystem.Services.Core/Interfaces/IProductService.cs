namespace StoreManagementSystem.Services.Core.Interfaces
{
    using ViewModels.Product;
    public interface IProductService
    {

        Task<IEnumerable<ProductMinViewModel>> GetAllProductsAsync();


        Task<IEnumerable<ProductMinViewModel>?> GetProductsByCategoryIdAsync(int categoryId);

        Task<ProductDetailsViewModel?> GetProductDetailsByIdAsync(int productId);

        Task<IEnumerable<ProductAddCategoryViewModel>> GetAllCategoriesAsync();

        Task<IEnumerable<ProductAddSupplierViewModel>> GetAllSuppliersAsync();

        Task<ProductAddInputModel> GetEmptyProductInputModelAsync();

        Task<bool> ProductExistsAsync(int productId);

        Task<bool> CategoryExistsAsync(int categoryId);

        Task<bool> SupplierExistsAsync(int supplierId);

        Task<ProductAddInputModel?> GetProductInputModelByProductIdAsync(int productId);

        Task EditProductAsync(ProductAddInputModel inputModel, int productId);

        Task CreateProductAsync(ProductAddInputModel inputModel);

        Task DeleteProductAsync(int productId);
    }
}
