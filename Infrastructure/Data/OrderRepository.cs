using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrders(string userId)
        {
           
            var orders = await _context.Order
                        .Where(o => o.UserId == userId)
                        .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrderById(int orderId, string userId)
        {

            var orders = await _context.Order
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product) 
                .Where(o => o.UserId == userId && o.Id == orderId)
                .ToListAsync();   
            return orders;
        }

        public async Task SaveOrder(Order order)
        {
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public Task UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

      
    }
}
