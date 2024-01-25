﻿namespace Core.Entites
{
    public class OrderItem:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
   
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}