namespace StoreManagementSystem.Services.Core
{
    using Data;
    using Data.Models;
    using Interfaces;
    using ViewModels.Admin.User;
    using static GCommon.OutputMessages.AdminUser;
    using static GCommon.OutputMessages.User;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class UserService : IUserService
    {
        private readonly StoreDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public UserService(StoreDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

        public async Task<UserInputModel> GetUserEditInputModel(string userId)
        {
            User? user = await dbContext
                .Users
                .FindAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException(UserNotFoundMsg);
            }

            ICollection<IdentityRole> roles = roleManager.Roles.ToList();

            ICollection<string> userRole = await userManager.GetRolesAsync(user);

            string? userRoleId = roles.FirstOrDefault(r => userRole.Contains(r.Name!))?.Id;

            UserInputModel inputModel = new UserInputModel()
            {
                Id = user.Id,
                UserName = user.UserName!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                RoleId = userRoleId ?? string.Empty,
                Roles = GetAllRoles()
            };

            return inputModel;
        }

        public Task EditUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoleViewModel> GetAllRoles()
        {
            IEnumerable<UserRoleViewModel> allRoles = roleManager
                .Roles
                .Select(r => new UserRoleViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name!
                })
                .ToList();

            return allRoles;
        }
    }
}
