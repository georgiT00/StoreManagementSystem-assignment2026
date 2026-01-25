namespace StoreManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidation;
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        [ForeignKey(nameof(ProductItem))]
        public int? ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }


        [Column(TypeName = UnitPriceType)]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }

}
