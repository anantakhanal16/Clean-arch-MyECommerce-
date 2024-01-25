using Core.Entites;

namespace Core.Interface.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrderById(int orderId ,string userId);
        Task<IEnumerable<Order>> GetAllOrders(string userId);
        Task SaveOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int orderId);
    }
}
