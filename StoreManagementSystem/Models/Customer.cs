namespace StoreManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidation;

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CustomerFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(CustomerLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(CustomerEmailRegex)]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(CustomerPhoneNumberRegex)]
        public string PhoneNumber { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
