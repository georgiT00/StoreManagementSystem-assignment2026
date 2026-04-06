namespace StoreManagementSystem.ViewModels.Admin.User
{
    using System.ComponentModel.DataAnnotations;

    using static GCommon.EntityValidation;

    public class UserInputModel
    {
        public string Id { get; set; } = null!;

        [Required]
        [MinLength(UserFirstNameMinLength)]
        [MaxLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(UserLastNameMinLength)]
        [MaxLength(UserLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;
    
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [RegularExpression(PhoneRegexPattern)]
        public string? PhoneNumber { get; set; }

        [Required] 
        public string RoleId { get; set; } = null!;


        //For dropdown lists in the form
        public IEnumerable<UserRoleViewModel> Roles { get; set; } =
            new HashSet<UserRoleViewModel>();
    }
}
