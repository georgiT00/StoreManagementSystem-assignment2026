namespace StoreManagementSystem.Services.Core
{
    using Data;
    using Data.Models;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ViewModels.Admin.User;

    public class UserService : IUserService
    {
        private readonly StoreDbContext dbContext;
        private readonly UserManager<User> userManager;

        public UserService(StoreDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<UserManageViewModel>> GetAllUsersAsync()
        {
            IEnumerable<User> users = await dbContext
                .Users
                .ToListAsync();

            ICollection<UserManageViewModel> userViewModel = 
                new List<UserManageViewModel>();

            foreach (User user in users)
            {
                IEnumerable<string> roles = await userManager.GetRolesAsync(user);

                userViewModel.Add(new UserManageViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Role = roles.FirstOrDefault() ?? "No Role" 
                });
            }

            return userViewModel;
        }
    }
}
