using Core.Entites;

namespace Core.Interface.Repositories
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> GetProductTypesAsync();
        Task<ProductType> GetProductTypeByIdAsync(int id);
        Task AddProductTypeAsync(ProductType productType);
        Task DeleteProductTypeAsync(int id);
        Task UpdateProductTypeAsync(ProductType productType);

    }
}
