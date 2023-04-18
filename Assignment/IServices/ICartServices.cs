using Assignment.Models;

namespace Assignment.IServices
{
    public interface ICartServices
    {
        public Cart GetCart(IServiceProvider services);
        public Task AddToCartAsync(Book book, int amount);
        public Task<int> RemoveFromCartAsync(Book book);
        public Task<List<CartDetail>> GetCartItemsAsync();
        public Task ClearCartAsync();
        public decimal GetShoppingCartTotal();
    }
}
