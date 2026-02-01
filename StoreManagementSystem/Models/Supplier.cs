namespace StoreManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidation;
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(SupplierNameMaxLength)]
        public string SupplierName { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
