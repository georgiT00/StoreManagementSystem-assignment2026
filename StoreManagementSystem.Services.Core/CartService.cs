namespace StoreManagementSystem.Services.Core
{
    using Interfaces;
    using Data;
    using Data.Models;
    using ViewModels.Cart;

    using Microsoft.EntityFrameworkCore;


    public class CartService : ICartService
    {
        private readonly StoreDbContext dbContext;

        public CartService(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateCartForUserAsync(string userId)
        {
            bool cartExists = await CartExistsForUserAsync(userId);

            if (!cartExists)
            {
                Cart cartToAdd = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };

                await dbContext.Carts.AddAsync(cartToAdd);
                await dbContext.SaveChangesAsync();
            }
        }


        public async Task AddToCartAsync(string userId, int productId)
        {
            CartItem itemToAdd = new CartItem
            {
                CartId = await GetCartIdByUserIdAsync(userId),
                ProductId = productId,
                Quantity = 1
            };

            await dbContext.CartItems.AddAsync(itemToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<CartDetailsViewModel?> GetCartDetailsByUserIdAsync(string userId)
        {
            CartDetailsViewModel? cart = await dbContext
                .Carts
                .Include(c => c.Items)
                 .ThenInclude(i => i.Product)
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .Select(c => new CartDetailsViewModel()
                {
                    Items = c.Items
                    .Select(i => new CartItemViewModel()
                    {
                        ProductId = i.ProductId != null ? i.ProductId.Value : 0,
                        ProductName = i.Product!.Name,
                        Price = i.Product.Price,
                        Quantity = i.Quantity
                    })
                    .ToList(),
                    TotalPrice = c.Items.Sum(i => i.Product!.Price * i.Quantity)
                })
                .SingleOrDefaultAsync();

            return cart;
        }

        public async Task<int> GetCartIdByUserIdAsync(string userId)
        {
            int cartId = await dbContext
                .Carts
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .Select(c => c.CartId)
                .SingleAsync();

            return cartId;
        }

        public async Task<bool> CartExistsForUserAsync(string userId)
        {
            bool cartExists = await dbContext
                .Carts
                .AsNoTracking()
                .AnyAsync(c => c.UserId == userId);

            return cartExists;
        }
    }
}
