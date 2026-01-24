namespace StoreManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public ICollection<ProductOrder> OrderProducts { get; set; } = new HashSet<ProductOrder>();
    }
}
