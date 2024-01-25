using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;
using System.Security.Claims;
using static NuGet.Packaging.PackagingConstants;

namespace Mye_CommerceApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            IEnumerable<Order> Orders = await _orderRepository.GetAllOrders(userId);

            var orderList = Orders.Select(order => new OrderList
            {
                OrderId=order.Id,
                UserId = order.UserId,
                ShippingAddress = order.ShippingAddress,
                TotalAmount = order.TotalAmount,
                DateTime = order.DateTime
            });
            return View(orderList);
        }

        public async Task<IActionResult> OrderDetails(int OrderId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            IEnumerable<Order> Orders = await _orderRepository.GetOrderById(OrderId, userId);

            var orderList = Orders.Select(order => new OrderList
            {
                ShippingAddress = order.ShippingAddress,
                TotalAmount = order.TotalAmount,
                DateTime = order.DateTime,
                OrderItems = order.OrderItems.Select(orderItem => new OrderItemDetail
                {
                    OrderId = orderItem.OrderId,
                    ProductName = orderItem.Product.Name,
                    Quantity = orderItem.Quantity,
                    UnitPrice = orderItem.UnitPrice,
                    TotalPrice = orderItem.TotalPrice
                }).ToList()
            });
            return View(orderList);
        }
      
    }
}
