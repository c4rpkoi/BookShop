using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Assignment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Assignment.Controllers
{
    public class CartController : Controller
    {
        private readonly IBooksServices _booksServices;
        private readonly ShopContext _context;
        private readonly Cart _shoppingCart;

        public CartController(IBooksServices booksServices, ShopContext context, Cart shoppingCart)
        {
            _booksServices = booksServices;
            _context = context;
            _shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _shoppingCart.GetCartDetailsAsync();
            _shoppingCart.CartDetails = items;

            var cartViewModel = new CartViewModel
            {
                Cart = _shoppingCart,
                CartTotal = _shoppingCart.GetCartTotal()
            };

            return View(cartViewModel);
        }
        
        public async Task<IActionResult> AddToShoppingCart(Guid bookID,int amount)
        {
            var selectedBook = await _booksServices.GetByIdAsync(bookID);

            if (selectedBook != null)
            {
                await _shoppingCart.AddToCartAsync(selectedBook, amount);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromShoppingCart(Guid bookID)
        {
            var selectedBook = await _booksServices.GetByIdAsync(bookID);

            if (selectedBook != null)
            {
                await _shoppingCart.RemoveFromCartAsync(selectedBook);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearCart()
        {
            await _shoppingCart.ClearCartAsync();

            return RedirectToAction("Index");
        }
    }
}
