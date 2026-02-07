namespace StoreManagementSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static GCommon.EntityValidation;
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(UserLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [ForeignKey(nameof(Supplier))]  
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        public virtual Cart Cart { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}
