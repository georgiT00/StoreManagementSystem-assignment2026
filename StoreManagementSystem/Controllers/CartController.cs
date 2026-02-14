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
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
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
                return NotFound();
            }

            await cartService.RemoveFromCartAsync(userId, id);
            return RedirectToAction("Index");
        }
    }
}
