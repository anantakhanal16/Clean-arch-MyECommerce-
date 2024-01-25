
using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Mye_CommerceApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;

        public CheckoutController(ICartRepository cartRepository,IOrderRepository orderRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CheckoutComplete(OrderDto OrderDetails)
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            IEnumerable<Cart> Cart = await _cartRepository.GetCartItemByIdAsync(UserId);

            if (Cart != null)
            {
                List<OrderItem> OrderItems = Cart.Select(cart => new OrderItem
                {
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    UnitPrice = cart.UnitPrice,
                    TotalPrice = cart.Quantity * cart.UnitPrice
                }).ToList();

                decimal totalAmount = OrderItems.Sum(item => item.TotalPrice);

                Order order = new Order
                {
                    ShippingAddress = OrderDetails.ShippingAddress,
                    UserId = UserId,
                    TotalAmount = totalAmount,
                    OrderItems = OrderItems
                };
                await _orderRepository.SaveOrder(order);

                await _cartRepository.ClearCartAsync(UserId);
            }
            return View();
        }


    }
}
