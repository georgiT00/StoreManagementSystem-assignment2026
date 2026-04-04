namespace StoreManagementSystem.Data.SeedData
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    using static GCommon.OutputMessages.Role;
    using static GCommon.OutputMessages.AdminUser;

    using Models;
    using Interfaces;

    using Microsoft.Extensions.Configuration;

    public class IdentityRoleSeed : IIdentityRoleSeed
    {
        private readonly IEnumerable<IdentityRole> roles = new IdentityRole[]
        {
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" },
            new IdentityRole { Name = "User", NormalizedName = "USER" }
        };

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public IdentityRoleSeed(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task SeedRolesAsync()
        {
            foreach (IdentityRole role in roles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role.Name);
                if (!roleExists)
                {
                    IdentityResult result = await roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(String.Format(RoleAddErrorMsg, role.Name));
                    }
                }
            }
        }

        public async Task SeedAdminUserAsync()
        {
            string? adminUserName = configuration["Admin:Username"] ?? 
                throw new InvalidOperationException(AdminUserNameNotFoundMsg);

            string? adminPassword = configuration["Admin:Password"] ?? 
                throw new InvalidOperationException(AdminUserPasswordNotMatchMsg);

            User? adminUser = await userManager.FindByNameAsync(adminUserName);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminUserName,
                    Email = adminUserName,
                    FirstName = "Admin",
                    LastName = "User",
                };

                IdentityResult result = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(String.Format(AdminUserAddErrorMsg, adminUserName));
                }
            }

            bool isInRole = await userManager.IsInRoleAsync(adminUser, "Admin");

            if (!isInRole)
            {
                IdentityResult result = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(String.Format(AdminUserAddErrorMsg, adminUserName));
                }
            }
        }
    }
}
