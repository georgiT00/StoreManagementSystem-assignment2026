namespace StoreManagementSystem.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;

    using static GCommon.EntityValidation;

    public class ProductAddInputModel
    {
        [Required]
        [MinLength(ItemNameMinLength)]
        [MaxLength(ItemNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(ItemPriceMinValue, ItemPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(ItemQuantityMinValue, ItemQuantityMaxValue)]
        public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; }


        public int? SupplierId { get; set; }


        //For dropdown lists in the form
        public IEnumerable<ProductCategoryViewModel> Categories { get; set; } 
            = new List<ProductCategoryViewModel>();

        public IEnumerable<ProductAddSupplierViewModel> Suppliers { get; set; } 
            = new List<ProductAddSupplierViewModel>();
    }
}
