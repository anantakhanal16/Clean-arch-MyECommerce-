using Core.Entites;
using Core.Interface.Repositories;

namespace Infrastructure.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddProductToCartAsync(Cart productDetail)
        {
            throw new NotImplementedException();
        }

        public Task ClearCartAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCartAsync()
        {
            throw new NotImplementedException();
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
