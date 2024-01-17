using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;

namespace Mye_CommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartService;

        private readonly IProductRepository _productService;

        public CartController(ICartRepository cartService, IProductRepository productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<IActionResult> GetCart()
        {
            var cartDetail = await _cartService.GetCartAsync();

            var cartDtos = cartDetail.Select(cart => new CartDto
            {
                ProductId = cart.ProductId,
                Product = cart.Product,
                ProductBrandId = cart.ProductBrandId,
                ProductBrand = cart.ProductBrand,
                ProductTypeId = cart.ProductTypeId,
                ProductType = cart.ProductType,
                UnitPrice = cart.UnitPrice,
                Quantity = cart.Quantity,
                TotalPrice = cart.TotalPrice
            });

            return View("~/Views/Cart/CartDetail.cshtml", cartDtos);

        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int quantity, int productId)
        {
                Product productDetail = await _productService.GetProductById(productId);

                if (productDetail != null)
                {
                    Cart cartitemDetail = new Cart
                    {
                        ProductTypeId = productDetail.ProductTypeId,
                        ProductBrandId = productDetail.ProductBrandId,
                        ProductId = productDetail.Id,
                        UnitPrice = productDetail.Price,
                        TotalPrice = productDetail.Price * quantity,
                        Quantity = quantity
                    };

                    await _cartService.AddProductToCartAsync(cartitemDetail);

                    return Json(new { success = true, message = "Item added to the cart successfully" });
                }

                return Json(new { success = false, message = "Product not found" });
            }
    }
}
