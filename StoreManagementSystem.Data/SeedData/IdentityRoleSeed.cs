namespace StoreManagementSystem.Data.SeedData
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    using static GCommon.OutputMessages.Role;
    using Models;
    using Interfaces;

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

        public IdentityRoleSeed(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
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
    }
}
