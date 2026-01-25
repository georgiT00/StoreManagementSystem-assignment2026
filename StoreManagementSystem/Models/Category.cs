namespace StoreManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidation;
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string CategoryName { get; set; } = null!;

        [MaxLength(CategoryDescriptionMaxLength)]
        public string Description { get; set; }

        public ICollection<ProductItem> Items { get; set; } = new HashSet<ProductItem>();
    }
}
