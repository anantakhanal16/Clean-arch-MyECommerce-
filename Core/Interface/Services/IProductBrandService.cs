using Core.Entites;

namespace Core.Interface.Services
{
    public interface IProductBrandService
    {
        Task<IEnumerable<ProductBrand>> GetProductsBrandAsync();
        Task<ProductBrand> GetProductBrandById(int id);
        Task AddProductBrandAsync(ProductBrand productBrand);
        Task DeleteProductBrandAsync(int id);
        Task UpdateProductBrandAsync(ProductBrand productBrand);
    }
}
