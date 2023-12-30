using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductTypeAsync(ProductType productType)
        {
            await _context.ProductType.AddAsync(productType);
        }

        public async Task DeleteProductTypeAsync(int id)
        {
           
            var productType = await _context.ProductType.FindAsync(id);
            if (productType != null)
            {
                _context.ProductType.Remove(productType);
            }
                            
        }

        public async Task<ProductType> GetProductTypeByIdAsync(int id)
        {
            return await _context.ProductType.FindAsync(id);
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            var productTypes = await _context.ProductType.ToListAsync();
            return productTypes;
        }

        public Task UpdateProductTypeAsync(ProductType productType)
        {
            throw new NotImplementedException();
        }
    }
}
