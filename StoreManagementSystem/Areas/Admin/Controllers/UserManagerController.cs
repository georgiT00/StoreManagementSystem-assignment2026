namespace StoreManagementSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Admin.User;

    public class UserManagerController : BaseAdminController
    {
        public IActionResult Index()
        {
            IEnumerable<UserManageViewModel> users = new List<UserManageViewModel>();
            return View(users);
        }
    }
}
