namespace StoreManagementSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Admin.User;
    using Services.Core.Interfaces;

    public class UserManagerController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserManagerController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserManageViewModel> userViewModel = await userService
                .GetAllUsersExcludeAdminAsync();

            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserInputModel userInputModel = await userService.GetUserEditInputModel(id);
            return View(userInputModel);
        }
    }
}
