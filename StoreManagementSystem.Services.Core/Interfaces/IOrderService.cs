namespace StoreManagementSystem.Services.Core.Interfaces
{
    using ViewModels.Order;
    public interface IOrderService
    {
        Task <IEnumerable<OrderDetailsViewModel>> GetOrdersForUserAsync(string userId);

        Task <int> GetOrdersCount(string userId);
    
    }
}
