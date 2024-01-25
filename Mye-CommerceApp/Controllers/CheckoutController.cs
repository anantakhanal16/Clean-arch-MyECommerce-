
using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;
using System.Security.Claims;
using System.Linq;
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
           
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IEnumerable<Cart> Cart = await _cartRepository.GetCartItemByIdAsync(UserId);
            
            List<OrderItem> OrderItems = new List<OrderItem>();

            if (Cart != null)
            {
                foreach (var cart in Cart)
                {
                    OrderItem orderItem = new OrderItem();
                    
                    orderItem.ProductId = cart.ProductId;
                    
                    orderItem.Quantity = cart.Quantity;

                    orderItem.UnitPrice = cart.UnitPrice;

                    orderItem.TotalPrice = cart.Quantity * cart.UnitPrice;

                    OrderItems.Add(orderItem);
                }

                Order order = new Order();

                order.ShippingAddress = OrderDetails.ShippingAddress;
                order.UserId = UserId;
                order.TotalAmount = Cart.Sum(item => item.Quantity * item.UnitPrice);
                order.OrderItems = OrderItems;
                
               await  _orderRepository.SaveOrder(order);
            }

            return View();
            
        }
    }
}
