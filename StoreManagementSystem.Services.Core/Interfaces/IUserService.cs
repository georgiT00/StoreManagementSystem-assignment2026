namespace StoreManagementSystem.Services.Core.Interfaces
{
    using ViewModels.Admin.User;

    public interface IUserService
    {
        Task<IEnumerable<UserManageViewModel>> GetAllUsersExcludeAdminAsync();
    }
}
