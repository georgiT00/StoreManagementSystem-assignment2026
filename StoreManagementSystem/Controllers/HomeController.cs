namespace StoreManagementSystem.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using StoreManagementSystem.Services.Core.Interfaces;
    using ViewModels;

    using static GCommon.AppConstants;

    public class HomeController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService)
        {
            this.orderService = orderService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if(User.Identity!.IsAuthenticated)
            {
                string userId = GetUserId();
                int ordersCount = await orderService.GetOrdersCount(userId);
                TempData[OrderCountTempDataKey] = ordersCount;
            }
            return View();
        }

        [Route("/Home/Error/{code}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int code)
        {
            if (code == StatusCodes.Status404NotFound)
            {
                return View("NotFound");
            }

            if (code == StatusCodes.Status400BadRequest)
            {
                return View("BadRequest");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
