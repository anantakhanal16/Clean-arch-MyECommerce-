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

        public async Task ClearCartAsync(string userId)
        {
            var cartItems = await _context.Cart
             .Where(c => c.AppUserId == userId)
             .ToListAsync();

             _context.Cart.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cart>> GetCartAsync(string currentUserId)
        {
             var cartDetail = await _context.Cart
                  .Where(c=> c.AppUserId == currentUserId)
                  .Include(p => p.Product)
                  .Include(p => p.ProductType)
                  .Include(p => p.ProductBrand)
                  .ToListAsync();
            return cartDetail;
        }

        public async Task<IEnumerable<Cart>> GetCartItemByIdAsync(string id)
        {

            var cartDetail = await _context.Cart
                 .Where(c => c.AppUserId == id)
                 .Include(p => p.Product)
                 .Include(p => p.ProductType)
                 .Include(p => p.ProductBrand)
               .ToListAsync();
            return cartDetail;

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
