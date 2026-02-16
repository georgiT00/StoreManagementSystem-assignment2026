namespace StoreManagementSystem.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using StoreManagementSystem.Services.Core.Interfaces;
    using ViewModels;

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
                TempData["OrdersCount"] = ordersCount;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
