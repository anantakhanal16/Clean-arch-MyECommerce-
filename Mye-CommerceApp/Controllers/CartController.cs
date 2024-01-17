using Microsoft.AspNetCore.Mvc;

namespace Mye_CommerceApp.Controllers
{
    public class CartController : Controller
    {

        public async Task<IActionResult> GetCartAsync()
        {
            return null;
        }

        public async Task<IActionResult> AddToCart(int quantity, int productId)
        {
            return null;
        }
    }
}
