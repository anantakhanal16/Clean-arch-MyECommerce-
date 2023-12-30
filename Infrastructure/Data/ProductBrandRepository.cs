using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductBrandRepository : IProductBrandRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductBrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBrandAsync(ProductBrand productBrand)
        {
            await _context.ProductBrand.AddAsync(productBrand);
        }

        public async Task DeleteBrandAsync(int id)
        {
            var productBrand = await _context.ProductBrand.FindAsync(id);
            if (productBrand != null)
            {
                _context.ProductBrand.Remove(productBrand);
            }
        }

        public async Task<ProductBrand> GetBrandByIdAsync(int id)
        {
            return await _context.ProductBrand.FindAsync(id);
        }

        public async Task<IEnumerable<ProductBrand>> GetBrandsAsync()
        {
            return await _context.ProductBrand.ToListAsync();
           
        }

        public Task UpdateBrandAsync(ProductBrand productBrand)
        {
            throw new NotImplementedException();
        }
    }
}
