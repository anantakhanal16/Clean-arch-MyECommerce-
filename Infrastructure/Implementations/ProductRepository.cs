using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var productDetail = await _context.Products
             .Include(p => p.ProductType)
             .Include(p => p.ProductBrand)
             .FirstOrDefaultAsync(p => p.Id == id);
            return productDetail;

        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string productTypeFilter, string productBrandFilter, decimal? minPriceFilter, decimal? maxPriceFilter)
        {
            IQueryable<Product> query = _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand);


            if (!string.IsNullOrEmpty(productTypeFilter))
            {
                query = query.Where(p => p.ProductType.Name == productTypeFilter);
            }
            if (!string.IsNullOrEmpty(productBrandFilter))
            {
                query = query.Where(p => p.ProductBrand.Name == productBrandFilter);
            }
            if (minPriceFilter.HasValue)
            {
                query = query.Where(p => p.Price >= minPriceFilter.Value);
            }
            if (maxPriceFilter.HasValue)
            {
                query = query.Where(p => p.Price <= maxPriceFilter.Value);
            }


            return await query.ToListAsync();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
