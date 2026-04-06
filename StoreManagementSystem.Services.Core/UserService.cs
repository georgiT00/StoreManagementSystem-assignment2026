namespace StoreManagementSystem.Services.Core
{
    using Data;
    using Data.Models;
    using Interfaces;
    using ViewModels.Admin.User;
    using static GCommon.OutputMessages.AdminUser;
    using static GCommon.OutputMessages.User;
    using static GCommon.OutputMessages.Role;

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
                .Where(u => u.UserName != adminUserName)
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
            };

            return inputModel;
        }

        public async Task EditUserAsync(UserInputModel inputModel, string userId)
        {
            User? user = dbContext.Users.Find(userId);

            if (user == null)
            {
                throw new InvalidOperationException(UserNotFoundMsg);
            }

            user.FirstName = inputModel.FirstName;
            user.LastName = inputModel.LastName;
            user.Email = inputModel.Email;
            user.UserName = inputModel.UserName;
            user.PhoneNumber = inputModel.PhoneNumber;

            IdentityRole? newRole = roleManager.Roles.FirstOrDefault(r => r.Id == inputModel.RoleId);

            if (newRole == null)
            {
                throw new InvalidOperationException();
            }

            string? currentRole = (await userManager
                .GetRolesAsync(user)).FirstOrDefault();

            if (currentRole != null && currentRole != newRole.Name)
            {
                await userManager.RemoveFromRoleAsync(user, currentRole);
                await userManager.AddToRoleAsync(user, newRole.Name!);
            }

            else if (currentRole == null)
            {
                await userManager.AddToRoleAsync(user, newRole.Name!);
            }

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException();
            }
        }

        public IEnumerable<UserRoleViewModel> GetAllRolesExceptAdmin()
        {
            IEnumerable<UserRoleViewModel> allRoles = roleManager
                .Roles
                .Where(r => r.Name != "Admin")
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
