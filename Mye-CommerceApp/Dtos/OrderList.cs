

namespace Mye_CommerceApp.Dtos
{
    public class OrderList
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public List<OrderItemDetail> OrderItems { get; set; } = new List<OrderItemDetail>();
    }

    public class OrderItemDetail
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; } 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
      
    }
}
