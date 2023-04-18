using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class CartServices : ICartServices
    {
        private readonly ShopContext _shopContext;
        private readonly UserManager<IdentityUser> _userManager;

        public CartServices(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public CartServices(ShopContext shopContext, UserManager<IdentityUser>? userManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
        }

        public async Task AddToCartAsync(Book book, int amount)
        {
            
        }

        public Task ClearCartAsync()
        {
            throw new NotImplementedException();
        }

        public Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<ShopContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            return new Cart(context) { CartID = Guid.Parse(cartId) };
        }

        public Task<List<CartDetail>> GetCartItemsAsync()
        {
            throw new NotImplementedException();
        }

        public decimal GetShoppingCartTotal()
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveFromCartAsync(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
