namespace StoreManagementSystem.ViewModels.Product
{
    public class ProductIndexViewModel
    {
        public IEnumerable<ProductMinViewModel> Products { get; set; } 
            = new List<ProductMinViewModel>();

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; } 
            = new List<ProductCategoryViewModel>();
    }
}
