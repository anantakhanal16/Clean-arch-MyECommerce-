using Core.Entites;

namespace Core.Interface.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrderById(int orderId);
        IEnumerable<Order> GetAllOrders();
        Task SaveOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int orderId);
    }
}
