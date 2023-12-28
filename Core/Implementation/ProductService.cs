using Core.Entites;
using Core.Interface.Repositories;
using Core.Interface.Services;

namespace Core.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _product;

        public ProductService(IProductRepository product)
        {
            _product= product;
        }

        public async Task AddProductAsync(Product product)
        {
            await _product.AddProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
           await _product.DeleteProductAsync(id);
        }

        public Task<Product> GetProductById(int id)
        {
            var product = _product.GetProductById(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
           var products =  await _product.GetProductsAsync();
            return products;
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
