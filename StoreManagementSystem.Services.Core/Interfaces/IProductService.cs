namespace StoreManagementSystem.Services.Core.Interfaces
{
    using ViewModels.Product;
    public interface IProductService
    {
        Task<IEnumerable<ProductMinViewModel>> GetAllProductsAsync();


        Task<IEnumerable<ProductMinViewModel>?> GetProductsByCategoryIdAsync(int categoryId);

        Task<ProductDetailsViewModel?> GetProductDetailsByIdAsync(int productId);

        Task<IEnumerable<ProductCategoryViewModel>> GetAllCategoriesAsync();

        Task<bool> CategoryExistsAsync(int categoryId);
    }
}
