using Assignment.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Component
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ShopContext _shopContext;

        public CategoryMenu(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _shopContext.Categories.OrderBy(c => c.Name).ToListAsync();
            return View(categories);
        }
    }
}
