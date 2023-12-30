using Core.Entites;
using Core.Interface.Repositories;
using Core.Interface.Services;

namespace Core.Implementation
{
    public class ProductBrandService : IProductBrandService
    {
        public readonly IProductBrandRepository _productBrand; 
        public ProductBrandService(IProductBrandRepository productBrand)
        {
            _productBrand = productBrand;
        }

        public async Task AddProductBrandAsync(ProductBrand productBrand)
        {
            await _productBrand.AddBrandAsync(productBrand);
        }

        public async Task DeleteProductBrandAsync(int id)
        {
            await _productBrand.DeleteBrandAsync(id);
        }

        public async Task<ProductBrand> GetProductBrandById(int id)
        {
           var product =  await _productBrand.GetBrandByIdAsync(id);
            return product;
        }

        public async Task<IEnumerable<ProductBrand>> GetProductsBrandAsync()
        {
            var productsBrands = await _productBrand.GetBrandsAsync();
            return productsBrands;

        }

        public Task UpdateProductBrandAsync(ProductBrand productBrand)
        {
            throw new NotImplementedException();
        }
    }
}
