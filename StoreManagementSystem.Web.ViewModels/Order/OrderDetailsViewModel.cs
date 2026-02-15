namespace StoreManagementSystem.ViewModels.Order
{
    using ViewModels.Cart;

    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TotalAmount { get; set; } = null!;
        public string Status { get; set; } = null!;
        public IEnumerable<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
    }
}
