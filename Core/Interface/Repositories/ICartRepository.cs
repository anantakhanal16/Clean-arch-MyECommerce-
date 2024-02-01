using Core.Entites;

namespace Core.Interface.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetCartAsync(string currentUserId);
        Task<Cart> GetCartItemByIdAsync(int itemId);
        Task AddProductToCartAsync(Cart productDetail);
        Task RemoveCartItemAsync(int id, string currentUserId);
        Task UpdateCartItemAsync(Cart UpdatedproductDetail);
        Task ClearCartAsync(string id);
    }
}
