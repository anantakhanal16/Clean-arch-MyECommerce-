using Core.Entites;

namespace Core.Interface.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(string productTypeFilter, string productBrandFilter, decimal? minPriceFilter, decimal? maxPriceFilter);
        Task<Product> GetProductById(int id);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task UpdateProductAsync(Product product);
    }
}
