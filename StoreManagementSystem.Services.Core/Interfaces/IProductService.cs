namespace StoreManagementSystem.Services.Core.Interfaces
{
    using ViewModels.Product;
    public interface IProductService
    {
        Task<IEnumerable<ProductMinViewModel>> GetAllProductsAsync();

        //Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int productId);
            
        //Task AddProductAsync(ProductFormModel model);
    
        //Task EditProductAsync(int productId, ProductFormModel model);
    
        //Task DeleteProductAsync(int productId);
    }
}
