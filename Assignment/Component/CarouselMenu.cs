using Assignment.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Component
{
    //[ViewComponent(Name = "Carouselmenu")]
    public class CarouselMenu : ViewComponent
    {
        private readonly ShopContext _context;

        public CarouselMenu(ShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _context.Books.Where(x=>x.IsBookOfTheWeek).ToListAsync();
            return View(books);
        }
    }
}
