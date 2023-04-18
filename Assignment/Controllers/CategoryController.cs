using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ShopContext _shopContext;
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ShopContext shopContext, ICategoryServices categoryServices)
        {
            _shopContext = shopContext;
            _categoryServices = categoryServices;
        }

        //GET: Category
        public async Task<IActionResult> Index()
        {
            return View(await _categoryServices.GetAllAsync());
        }

        //GET: Category/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //Get: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryServices.Add(category);
                await _categoryServices.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET: Category/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _categoryServices.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST:Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryID,Name,Description")] Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryServices.Update(category);
                    await _categoryServices.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();
            return View(category);
        }

        //GET: Category/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id ==null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetByIdAsync(id);

            if (category==null)
            {
                return NotFound();
            }

            return View(category);
        }

        //POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComfirmed(Guid id)
        {
            var category = await _categoryServices.GetByIdAsync(id);
            _categoryServices.Remove(category);
            await _categoryServices.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool CategoryExists(Guid id)
        {
            return _categoryServices.Exists(id);
        }
    }
}
