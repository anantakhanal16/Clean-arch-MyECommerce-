using Core.Entites;

namespace Mye_CommerceApp.Dtos
{
    public class CartDto
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}

