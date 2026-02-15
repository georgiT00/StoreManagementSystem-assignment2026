namespace StoreManagementSystem.ViewModels.Cart
{
    public class CartDetailsViewModel
    {
        public IEnumerable<CartItemViewModel> Items { get; set; } 
            = new List<CartItemViewModel>();

        public string TotalPrice { get; set; } = null!;
    }
}
