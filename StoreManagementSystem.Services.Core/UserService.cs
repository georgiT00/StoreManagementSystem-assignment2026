namespace StoreManagementSystem.Services.Core
{
    using Data;
    using Data.Models;
    using Interfaces;
    using ViewModels.Admin.User;
    using static GCommon.OutputMessages.AdminUser;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class UserService : IUserService
    {
        private readonly StoreDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public UserService(StoreDbContext dbContext, UserManager<User> userManager, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<UserManageViewModel>> GetAllUsersExcludeAdminAsync()
        {
            string? adminUserName = configuration["Admin:Username"] ?? 
                throw new InvalidOperationException(AdminUserNameNotFoundMsg);

            IEnumerable<User> users = await dbContext
                .Users
                .Except(dbContext.Users.Where(u => u.UserName == adminUserName))
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
