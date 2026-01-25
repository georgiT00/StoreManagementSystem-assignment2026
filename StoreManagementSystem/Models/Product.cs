namespace StoreManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidation;
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(ItemNameMaxLength)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = ItemPriceType)]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(Supplier))]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
