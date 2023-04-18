using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AuthorController : Controller
    {
        private readonly ShopContext _shopContext;

        public AuthorController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        // GET: Author
        public async Task<IActionResult> Index()
        {
            return View(await _shopContext.Authors.ToListAsync());
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _shopContext.Authors
                .SingleOrDefaultAsync(m => m.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorID,Name")] Author author)
        {
            if (ModelState.IsValid)
            {
                _shopContext.Add(author);
                await _shopContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _shopContext.Authors.SingleOrDefaultAsync(m => m.AuthorID == id);
            if (ingredients == null)
            {
                return NotFound();
            }
            return View(ingredients);
        }

        // POST: Author/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorID,Name")] Author author)
        {
            if (id != author.AuthorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _shopContext.Update(author);
                    await _shopContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorID))
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
            return View(author);
        }

        // GET: Author/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _shopContext.Authors
                .SingleOrDefaultAsync(m => m.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var author = await _shopContext.Authors.SingleOrDefaultAsync(m => m.AuthorID == id);
            _shopContext.Authors.Remove(author);
            await _shopContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AuthorExists(Guid id)
        {
            return _shopContext.Authors.Any(e => e.AuthorID == id);
        }
    }
}
