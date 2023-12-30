using Core.Entites;
using Core.Interface.Repositories;
using Core.Interface.Services;

namespace Core.Implementation
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productType;

        public ProductTypeService(IProductTypeRepository productType)
        {
            _productType= productType;
        }

        public async Task AddProductTypeAsync(ProductType productType)
        {
           await  _productType.AddProductTypeAsync(productType);
        }

        public async Task DeleteProductTypeAsync(int id)
        {
            await _productType.DeleteProductTypeAsync(id);
        }

        public async Task<ProductType> GetProductBrandById(int id)
        {
           var productType=  await _productType.GetProductTypeByIdAsync(id);
           
            return productType;
        }

        public async Task<IEnumerable<ProductType>> GetProductsTypeAsync()
        {
            var productTypes = await _productType.GetProductTypesAsync();
            
            return productTypes;
        }

        public Task UpdateProductTypeAsync(ProductType productType)
        {
            throw new NotImplementedException();
        }
    }
}
