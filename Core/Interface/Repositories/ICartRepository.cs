using Core.Entites;

namespace Core.Interface.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetCartAsync(string currentUserId);
        Task<IEnumerable<Cart>> GetCartItemByIdAsync(string id);
        Task AddProductToCartAsync(Cart productDetail);
        Task RemoveCartItemAsync(int id);
        Task UpdateCartItemAsync(Cart UpdatedproductDetail);
        Task ClearCartAsync(string id);
    }
}
