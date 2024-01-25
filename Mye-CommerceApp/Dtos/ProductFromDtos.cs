
namespace Mye_CommerceApp.Dtos
{
    public class ProductFromDtos
    {
        public List<ProductTypeDto> ProductTypes = new List<ProductTypeDto>();
        
        public List<ProductBrandDto> ProductBrand = new List<ProductBrandDto>();
        public ProductForm Product { get; set; }

        public List<string> ErrorMessages = new List<string>();
    }

    public class ProductForm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }
        public IFormFile PictureFile { get; set; }
    }
}
