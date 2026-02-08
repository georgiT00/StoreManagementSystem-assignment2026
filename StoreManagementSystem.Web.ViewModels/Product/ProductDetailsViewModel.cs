namespace StoreManagementSystem.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string Price { get; set; } = null!;

        public int Quantity { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? SupplierName { get; set; }
    }
}