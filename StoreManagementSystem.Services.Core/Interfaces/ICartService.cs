namespace StoreManagementSystem.Services.Core.Interfaces
{
    using ViewModels.Cart;

    public interface ICartService
    {
        Task <CartDetailsViewModel?> GetCartDetailsByUserIdAsync(string userId);

        Task AddToCartAsync(string userId, int productId);

        Task<int> GetCartIdByUserIdAsync(string userId);

        Task CreateCartForUserAsync(string userId);

        Task<bool> CartExistsForUserAsync(string userId);
    }
}
