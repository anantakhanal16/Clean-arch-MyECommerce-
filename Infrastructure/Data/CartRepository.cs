using Core.Entites;
using Core.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductToCartAsync(Cart productDetail)
        {
            await _context.AddAsync(productDetail);
            _context.SaveChanges();
        }

        public Task ClearCartAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cart>> GetCartAsync()
        {
            var cartDetail= await _context.Cart
                  .Include(p => p.Product)
                  .Include(p => p.ProductType)
                  .Include(p => p.ProductBrand)
                .ToListAsync();
            return cartDetail;
           
        }

        public Task<Cart> GetCartItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCartItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartItemAsync(Cart UpdatedproductDetail)
        {
            throw new NotImplementedException();
        }
    }
}
