using Assignment.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Models
{
    public class Cart
    {
        private readonly ShopContext _shopContext;

        public Cart(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }


        public Guid CartID { get; set; }
       
        public  List<CartDetail>? CartDetails { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<ShopContext>();
            string cartId = session.GetString("CartId")??Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            return new Cart(context) { CartID= Guid.Parse(cartId)};
        }
        public async Task AddToCartAsync(Book book, int amount)
        {
            var cartItem= await _shopContext.CartDetails.SingleOrDefaultAsync(
                s=>s.Books.ID== book.ID&& s.CartID == CartID);
            var localAmount = 0;
            if (cartItem==null)
            {
                cartItem = new CartDetail
                {
                    CartID = CartID,
                    Books = book,
                    Amount = amount
                };
                _shopContext.CartDetails.Add(cartItem);
            }
            else
            {
                cartItem.Amount+=amount;
            }
            await _shopContext.SaveChangesAsync();
        }

        public async Task<int> RemoveFromCartAsync(Book book)
        {
            var cartItem = await _shopContext.CartDetails.SingleOrDefaultAsync(
                s => s.Books.ID == book.ID && s.CartID == CartID);
            var localAmount = 0;
            if (cartItem!= null)
            {
                if (cartItem.Amount>1)
                {
                    cartItem.Amount--;
                    localAmount = cartItem.Amount;
                }
                else
                {
                    _shopContext.CartDetails.Remove(cartItem);
                }
            }
            await _shopContext.SaveChangesAsync();
            return localAmount;
        }

        public async Task<List<CartDetail>> GetCartDetailsAsync()
        {
            return CartDetails ??
                (CartDetails = await
                    _shopContext.CartDetails.Where(c=>c.CartID == CartID)
                        .Include(s=>s.Books)
                        .ToListAsync());
        }

        public async Task ClearCartAsync()
        {
            var cartDetails = _shopContext.CartDetails.Where(cart => cart.CartID == CartID);
            _shopContext.CartDetails.RemoveRange(cartDetails);
            await _shopContext.SaveChangesAsync();
        }
        public decimal GetCartTotal()
        {
            var total = _shopContext.CartDetails.Where(c=>c.CartID == CartID)
                .Select(c=>c.Books.Price* c.Amount).Sum();
            return total;
        }

    }
}
