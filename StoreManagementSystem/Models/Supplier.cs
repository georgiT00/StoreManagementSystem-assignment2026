using System.ComponentModel.DataAnnotations;

namespace StoreManagementSystem.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(20)]
        public string SupplierName { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
