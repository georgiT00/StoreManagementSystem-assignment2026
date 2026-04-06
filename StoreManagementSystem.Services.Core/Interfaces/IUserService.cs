namespace StoreManagementSystem.Services.Core.Interfaces
{
    using ViewModels.Admin.User;

    public interface IUserService
    {
        Task<IEnumerable<UserManageViewModel>> GetAllUsersExcludeAdminAsync();

        Task<UserInputModel> GetUserEditInputModel(string userId);

        IEnumerable<UserRoleViewModel> GetAllRoles();

        Task EditUserAsync(UserInputModel inputModel, string userId);
    }
}
