namespace StoreManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class BaseController : Controller
    {
        protected string GetUserId()
        {
            return User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
