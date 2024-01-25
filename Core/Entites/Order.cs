using Core.Entites.Identity;

namespace Core.Entites
{
    public class Order: BaseEntity
    {
        public string UserId { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime  DateTime { get; set; } = DateTime.UtcNow;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
