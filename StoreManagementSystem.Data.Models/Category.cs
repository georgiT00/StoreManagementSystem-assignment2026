namespace StoreManagementSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static GCommon.EntityValidation;
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string CategoryName { get; set; } = null!;

        [MaxLength(CategoryDescriptionMaxLength)]
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
