namespace StoreManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    using Enums;
    using static Common.EntityValidation;
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public CategoryType Type { get; set; }

        [Required]
        [MaxLength(CategoryDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}
