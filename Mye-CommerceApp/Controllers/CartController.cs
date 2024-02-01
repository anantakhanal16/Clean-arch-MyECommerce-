using Core.Entites;
using Core.Interface.Repositories;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;
using System.Security.Claims;

namespace Mye_CommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartService;

        private readonly IProductRepository _productService;

        private readonly AppIdentityDbcontext _user;

        public CartController(ICartRepository cartService, IProductRepository productService, AppIdentityDbcontext user)
        {
            _cartService = cartService;
            _productService = productService;
            //_user = user;
        }

        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartDetail = await _cartService.GetCartAsync(currentUserId);

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
        [Authorize]
        public async Task<IActionResult> AddToCart(int quantity, int productId)
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cartDetail = await _cartService.GetCartAsync(currentUserId);

            Cart existingCartItem = cartDetail.FirstOrDefault(product => product.ProductId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;

                existingCartItem.TotalPrice = existingCartItem.UnitPrice * existingCartItem.Quantity;
                
                await _cartService.UpdateCartItemAsync(existingCartItem);

                return Json(new { success = true, message = "Item added to the cart successfully" });
            }
            else
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
                        Quantity = quantity,
                        AppUserId = currentUserId
                    };

                    await _cartService.AddProductToCartAsync(cartitemDetail);

                    return Json(new { success = true, message = "Item added to the cart successfully" });
                }

            }

            return Json(new { success = false, message = "Product not found" });
         }

       
        public async Task<IActionResult> DeleteCartItem(int productId)
        {
             string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

             await _cartService.RemoveCartItemAsync(productId, currentUserId);

            return RedirectToAction("GetCart");
        }

        public async Task<IActionResult> UpdateCartItem(int quantity, int productId)
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cartDetail = await _cartService.GetCartAsync(currentUserId);

            Cart existingCartItem = cartDetail.FirstOrDefault(product => product.ProductId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity = quantity;

                existingCartItem.TotalPrice = existingCartItem.UnitPrice * quantity;

                await _cartService.UpdateCartItemAsync(existingCartItem);

                return Json(new { success = true, message = "Item has been updated" });
            }
            else 
            {
                return Json(new { success = true, message = "Error" });
            }

        }
    }
}
