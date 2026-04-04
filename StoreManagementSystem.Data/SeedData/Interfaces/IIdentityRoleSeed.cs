namespace StoreManagementSystem.Data.SeedData.Interfaces
{
    public interface IIdentityRoleSeed
    {
        Task SeedRolesAsync();

        Task SeedAdminUserAsync();
    }
}
