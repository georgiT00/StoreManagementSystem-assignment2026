using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagementSystem.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        [ForeignKey(nameof(ProductItem))]
        public int? ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }

        public int Quantity { get; set; }
    }

}
