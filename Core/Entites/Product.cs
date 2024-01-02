using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entites
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public int ProductTypeId { get; set; }
        
        public ProductType ProductType { get; set; }

        public ProductBrand ProductBrand { get; set; }

        public int ProductBrandId { get; set; }

        public string ProductImageName { get; set; }
    }
}
