using Core.Entites;

namespace Core.Interface.Repositories
{
    public interface IProductBrandRepository
    {
        Task<IEnumerable<ProductBrand>> GetBrandsAsync();
        Task<ProductBrand> GetBrandByIdAsync(int id);
        Task AddBrandAsync(ProductBrand productBrand);
        Task DeleteBrandAsync(int id);
        Task UpdateBrandAsync(ProductBrand productBrand);
    }
}
