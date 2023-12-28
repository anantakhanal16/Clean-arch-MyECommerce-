using Core.Entites;

namespace Core.Interface.Services
{
    public interface IProductTypeService
    {
        Task<IEnumerable<ProductType>> GetProductsTypeAsync();
        Task<ProductType> GetProductBrandById(int id);
        Task AddProductTypeAsync(ProductType productType);
        Task DeleteProductTypeAsync(int id);
        Task UpdateProductTypeAsync(ProductType productType);
    }
}
