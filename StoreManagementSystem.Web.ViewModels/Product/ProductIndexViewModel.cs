namespace StoreManagementSystem.ViewModels.Product
{
    public class ProductIndexViewModel
    {
        public IEnumerable<ProductMinViewModel> Products { get; set; } 
            = new List<ProductMinViewModel>();

        public IEnumerable<ProductAddCategoryViewModel> Categories { get; set; } 
            = new List<ProductAddCategoryViewModel>();
    }
}
