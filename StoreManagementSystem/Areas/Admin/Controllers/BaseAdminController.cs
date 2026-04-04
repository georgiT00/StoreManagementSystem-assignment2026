namespace StoreManagementSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Client;
    using System.Security.Claims;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BaseAdminController : Controller
    {
        protected string GetAdminUserId()
        {
            string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }
    }
}
