using Assignment.Models;
using Assignment.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Component
{
    public class ShoppingCartSummary :ViewComponent
    {
        private readonly Cart cart;

        public ShoppingCartSummary(Cart cart)
        {
            this.cart = cart;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await cart.GetCartDetailsAsync();
            cart.CartDetails = items;

            var CartViewModel = new CartViewModel
            {
                Cart = cart,
                CartTotal = cart.GetCartTotal(),
            };
            return View(CartViewModel);
        }
    }
}
