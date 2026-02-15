namespace StoreManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Order;
    using Services.Core.Interfaces;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId();
            IEnumerable<OrderDetailsViewModel> viewModel = await orderService.GetOrdersForUserAsync(userId);

            return View(viewModel);
        }
    }
}
