using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using Assignment.Data;
using Assignment.Models;

namespace Assignment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorBooksController : Controller
    {

        private readonly ShopContext shopContext;

        public AuthorBooksController(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }



        // GET: AuthorBooks
        public async Task<IActionResult> Index()
        {
            var appDbContext = shopContext.AuthorBooks.Include(p => p.Author).Include(p => p.Books);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AuthorBooks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorBooks = await shopContext.AuthorBooks
                .Include(p => p.Author)
                .Include(p => p.Books)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (authorBooks == null)
            {
                return NotFound();
            }

            return View(authorBooks);
        }

        // GET: AuthorBooks/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(shopContext.Authors, "AuthorID", "Name");
            ViewData["BookID"] = new SelectList(shopContext.Books, "ID", "Name");
            return View();
        }

        // POST: AuthorBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookID,AuthorID")] AuthorBooks authorBooks)
        {
            if (ModelState.IsValid)
            {
                shopContext.Add(authorBooks);
                await shopContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();
            ViewData["AuthorID"] = new SelectList(shopContext.Authors, "AuthorID", "Name", authorBooks.AuthorID);
            ViewData["BookID"] = new SelectList(shopContext.Books, "ID", "Name", authorBooks.BookID);
            return View(authorBooks);
        }

        // GET: AuthorBooks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorBooks = await shopContext.AuthorBooks.SingleOrDefaultAsync(m => m.Id == id);
            if (authorBooks == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(shopContext.Authors, "AuthorID", "Name", authorBooks.AuthorID);
            ViewData["BookID"] = new SelectList(shopContext.Books, "ID", "Name", authorBooks.BookID);
            return View(authorBooks);
        }

        // POST: AuthorBooks/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BookID,AuthorID")] AuthorBooks authorBooks)
        {
            if (id != authorBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    shopContext.Update(authorBooks);
                    await shopContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorBooksExists(authorBooks.Id))
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
            ViewData["AuthorID"] = new SelectList(shopContext.Authors, "AuthorID", "Name", authorBooks.AuthorID);
            ViewData["BookID"] = new SelectList(shopContext.Books, "ID", "Name", authorBooks.BookID);
            return View(authorBooks);
        }

        // GET: AuthorBooks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorBooks = await shopContext.AuthorBooks
                .Include(p => p.Author)
                .Include(p => p.Books)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (authorBooks == null)
            {
                return NotFound();
            }

            return View(authorBooks);
        }

        // POST: AuthorBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var authorBooks = await shopContext.AuthorBooks.SingleOrDefaultAsync(m => m.Id == id);
            shopContext.AuthorBooks.Remove(authorBooks);
            await shopContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AuthorBooksExists(Guid id)
        {
            return shopContext.AuthorBooks.Any(e => e.Id == id);
        }

    }
}
