using Core.Entites;

namespace Core.Interface.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetCartAsync(string currentUserId);
        Task<Cart> GetCartItemByIdAsync(int id);
        Task AddProductToCartAsync(Cart productDetail);
        Task RemoveCartItemAsync(int id);
        Task UpdateCartItemAsync(Cart UpdatedproductDetail);
        Task ClearCartAsync();
    }
}
