namespace StoreManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Interfaces;
    using System.Threading.Tasks;
    using ViewModels.Cart;
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        private readonly ILogger<CartController> logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            this.cartService = cartService;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            CartDetailsViewModel viewModel = await cartService
                .GetCartDetailsByUserIdAsync(userId);

            return View(viewModel);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            bool isProductInCart = await cartService.IsProductInCartAsync(userId, id);

            if (!isProductInCart)
            {
                TempData["ErrorMessage"] = "This product is not in your cart.";
                return RedirectToAction("Index");
            }

            try
            {
                await cartService.RemoveFromCartAsync(userId, id);
            }

            catch(InvalidOperationException exception)
            {
                logger.LogError(exception, $"Error removing product {id} from cart for user {userId}");
                TempData["ErrorMessage"] = exception.Message;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder()
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await cartService.CreateOrderAsync(userId);
            }

            catch(InvalidOperationException exception)
            {
                logger.LogError(exception, $"Error placing order for user {userId}");
                TempData["ErrorMessage"] = exception.Message;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Order");
        }
    }
}
