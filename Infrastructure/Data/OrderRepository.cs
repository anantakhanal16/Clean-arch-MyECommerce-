using Core.Entites;
using Core.Interface.Repositories;

namespace Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveOrder(Order order)
        {
            try
            {
                var id= await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();


               
            }
            catch { }
          

        }

        Task IOrderRepository.DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        Task IOrderRepository.UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
