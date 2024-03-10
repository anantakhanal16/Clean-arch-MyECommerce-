using Core.Entites;

namespace Mye_CommerceApp.Dtos
{
    public class Home
    {
        public List<ProductListDto> productList = new List<ProductListDto>();

        public List<ProductBrandList> productbrandList = new List<ProductBrandList>();

        public List<ProducttypeList> producttypelist = new List<ProducttypeList>();

        public string? SelectedProductTypeFilter { get; set; }
        public string? SelectedProductBrandFilter { get; set; }
        public decimal? MinPriceFilter { get; set; }
        public decimal? MaxPriceFilter { get; set; }
    }


    public class ProducttypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class ProductBrandList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }

    }

}
