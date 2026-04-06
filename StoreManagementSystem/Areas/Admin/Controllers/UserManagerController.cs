namespace StoreManagementSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Admin.User;
    using Services.Core.Interfaces;

    using static GCommon.OutputMessages.User;

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

            userInputModel.Roles = userService.GetAllRolesExceptAdmin();
            return View(userInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserInputModel inputModel, [FromRoute] string id)
        { 
            IEnumerable<UserRoleViewModel> allRoles = userService.GetAllRolesExceptAdmin();

            if (inputModel.RoleId == null || !allRoles.Any(r => r.RoleId == inputModel.RoleId))
            {
                ModelState.AddModelError(nameof(inputModel.RoleId), "Selected role does not exist.");
                inputModel.Roles = allRoles;
                return View(inputModel);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, String.Format(UserEditErrorMsg, inputModel.UserName));

                inputModel.Roles = allRoles;

                return View(inputModel);
            }

            try
            {
                await userService.EditUserAsync(inputModel, id);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, String.Format(UserEditErrorMsg, inputModel.UserName));

                inputModel.Roles = allRoles;

                return View(inputModel);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
