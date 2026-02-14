namespace StoreManagementSystem.Services.Core.Interfaces
{
    using ViewModels.Cart;

    public interface ICartService
    {
        Task CreateCartForUserAsync(string userId);

        Task <CartDetailsViewModel?> GetCartDetailsByUserIdAsync(string userId);

        Task AddToCartAsync(string userId, int productId);

        Task RemoveFromCartAsync(string userId, int productId);

        Task<int> GetCartIdByUserIdAsync(string userId);

        Task<bool> CartExistsForUserAsync(string userId);
        
        Task<bool> IsProductInCartAsync(string userId, int productId);
    }
}
