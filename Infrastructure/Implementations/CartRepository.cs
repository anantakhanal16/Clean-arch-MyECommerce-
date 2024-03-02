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
            var cartItems = await _context.Cart.Where(c => c.AppUserId == userId).ToListAsync();

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

        public async Task<Cart> GetCartItemByIdAsync(int productId)
        {
            var cartItem = await _context.Cart.FirstOrDefaultAsync(c => c.ProductId == productId);

            return cartItem;
        }

        public async Task RemoveCartItemAsync(int id,  string currentUserId)
        {
            var product = await _context.Cart
            .Where(c => c.AppUserId == currentUserId && c.Product.Id ==id)
            .ToListAsync();
            if (product != null)
            {
                _context.Cart.RemoveRange(product);

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItemAsync(Cart UpdatedproductDetail)
        {
            var existingCart = await GetCartItemByIdAsync(UpdatedproductDetail.ProductId);

            if (existingCart != null)
            {
                existingCart.Quantity = UpdatedproductDetail.Quantity;
                existingCart.TotalPrice = UpdatedproductDetail.TotalPrice;
            }
            await _context.SaveChangesAsync();

        }

    }
}
