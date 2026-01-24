namespace StoreManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidation;
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ItemNameMaxLength)]
        public string Name { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = ItemPriceType)]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<ProductOrder> ProductOrders { get; set; } = new HashSet<ProductOrder>();
    }
}
