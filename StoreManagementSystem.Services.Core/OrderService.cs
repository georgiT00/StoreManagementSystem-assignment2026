namespace StoreManagementSystem.Services.Core
{
    using Interfaces;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ViewModels.Order; 

    using System.Threading.Tasks;
    using StoreManagementSystem.ViewModels.Cart;
    using Microsoft.EntityFrameworkCore;

    public class OrderService : IOrderService
    {
        private readonly StoreDbContext dbContext;

        public OrderService(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task <IEnumerable<OrderDetailsViewModel>> GetOrdersForUserAsync(string userId)
        {
            IEnumerable<OrderDetailsViewModel> orderViewModel = await dbContext
                .Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .AsNoTracking()
                .Select(o => new OrderDetailsViewModel()
                {
                    OrderId = o.OrderId,
                    CreatedAt = o.CreatedAt,
                    Status = OrderStatus.Pending.ToString(),
                    TotalAmount = o.TotalAmount.ToString("F2"),
                    Items = o.Items
                    .Select(i => new CartItemViewModel()
                    {
                        ProductId = i.ProductId != null ? i.ProductId.Value : 0,
                        ProductName = i.Product!.Name,
                        Quantity = i.Quantity,
                        Price = i.UnitPrice
                    }).ToList(),
                })
                .ToListAsync();

            return orderViewModel;
        }
    }
}
