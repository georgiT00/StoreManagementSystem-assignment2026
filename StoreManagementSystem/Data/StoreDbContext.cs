namespace StoreManagementSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Cart> Carts { get; set; } = null!;

        public DbSet<CartItem> CartItems { get; set; } = null!; 

        public DbSet<Category> Categories { get; set; } = null!; 

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        public DbSet<Supplier> Suppliers { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
