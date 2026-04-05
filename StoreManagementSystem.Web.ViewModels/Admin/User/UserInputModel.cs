namespace StoreManagementSystem.ViewModels.Admin.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserInputModel
    {
        public string Id { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;
    
        [Required]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        [Required] 
        public string RoleId { get; set; } = null!;


        //For dropdown lists in the form
        public IEnumerable<UserRoleViewModel> Roles { get; set; } = null!;
    }
}
