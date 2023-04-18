using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Assignment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Assignment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {

        private readonly ShopContext _context;
        private readonly IBooksServices _booksServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ISupplierServices _supplierServices;

        public BooksController(ShopContext context, IBooksServices booksServices, ICategoryServices categoryServices, ISupplierServices supplierServices)
        {
            _context = context;
            _booksServices = booksServices;
            _categoryServices = categoryServices;
            _supplierServices = supplierServices;
        }




        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _booksServices.GetAllIncludedAsync());
        }

        // GET: Books
        [AllowAnonymous]
        public async Task<IActionResult> ListAll()
        {
            var model = new SearchBooksViewModel()
            {
                BookList = await _booksServices.GetAllIncludedAsync(),
                SearchText = null
            };

            return View(model);
        }

        private async Task<List<Book>> GetBookSearchList(string userInput)
        {
            var userInputTrimmed = userInput?.ToLower()?.Trim();

            if (string.IsNullOrWhiteSpace(userInputTrimmed))
            {
                return await _context.Books.Include(p => p.Category)
                    .Select(p => p).OrderBy(p => p.Name)
                    .ToListAsync();
            }
            else
            {
                return await _context.Books.Include(p => p.Category)
                    .Where(p => p
                    .Name.ToLower().Contains(userInputTrimmed))
                    .Select(p => p).OrderBy(p => p.Name)
                    .ToListAsync();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AjaxSearchList(string searchString)
        {
            var booksList = await GetBookSearchList(searchString);

            return PartialView(booksList);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ListAll([Bind("SearchText")] SearchBooksViewModel model)
        {
            var books = await _booksServices.GetAllIncludedAsync();
            if (model.SearchText == null || model.SearchText == string.Empty)
            {
                model.BookList = books;
                return View(model);
            }

            var input = model.SearchText.Trim();
            if (input == string.Empty || input == null)
            {
                model.BookList = books;
                return View(model);
            }
            var searchString = input.ToLower();

            if (string.IsNullOrEmpty(searchString))
            {
                model.BookList = books;
            }
            else
            {
                var booksList = await _context.Books.Include(x => x.Category).Include(x => x.Supplier).Include(x => x.AuthorBooks).OrderBy(x => x.Name)
                     .Where(p =>
                     p.Name.ToLower().Contains(searchString)
                  || p.Price.ToString("c").ToLower().Contains(searchString)
                  || p.Category.Name.ToLower().Contains(searchString)
                  || p.AuthorBooks.Select(x => x.Author.Name.ToLower()).Contains(searchString))
                    .ToListAsync();

                if (booksList.Any())
                {
                    model.BookList = booksList;
                }
                else
                {
                    model.BookList = new List<Book>();
                }

            }
            return View(model);
        }

        // GET: Books
        [AllowAnonymous]
        public async Task<IActionResult> ListCategory(string categoryName)
        {
            bool categoryExtist = _context.Categories.Any(c => c.Name == categoryName);
            if (!categoryExtist)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);

            if (category == null)
            {
                return NotFound();
            }

            bool anyBooks = await _context.Books.AnyAsync(x => x.Category == category);
            if (!anyBooks)
            {
                return NotFound($"Không có sách nào ở thể loại {categoryName}");
            }

            var books = _context.Books.Where(x => x.Category == category)
                .Include(x => x.Category).Include(x => x.Supplier);

            ViewBag.CurrentCategory = category.Name;
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _booksServices.GetByIdIncludedAsync(id);

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> DisplayDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _booksServices.GetByIdIncludedAsync(id);

            var listOfAuthorBooks = await _context.AuthorBooks.Where(x => x.BookID == id).Select(x => x.Author.Name).ToListAsync();
            ViewBag.AuthorBooks = listOfAuthorBooks;


            var supplierName = await _context.Suppliers.Where(x=>x.SupplierID== books.SupplierID).Select(x=>x.Name).ToListAsync();
            ViewBag.SuppliersName = supplierName;
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books
        [AllowAnonymous]
        public async Task<IActionResult> SearchBooks()
        {
            var model = new SearchBooksViewModel()
            {
                BookList = await _booksServices.GetAllIncludedAsync(),
                SearchText = null
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchBooks([Bind("SearchText")] SearchBooksViewModel model)
        {
            var books = await _booksServices.GetAllIncludedAsync();
            var search = model.SearchText.ToLower();

            if (string.IsNullOrEmpty(search))
            {
                model.BookList = books;
            }
            else
            {
                var booksList = await _context.Books.Include(x => x.Category).Include(x => x.Supplier).Include(x => x.AuthorBooks).OrderBy(x => x.Name)
                    .Where(p =>
                     p.Name.ToLower().Contains(search)
                  || p.Price.ToString("c").ToLower().Contains(search)
                  || p.Category.Name.ToLower().Contains(search)
                  || p.AuthorBooks.Select(x => x.Author.Name.ToLower()).Contains(search)).ToListAsync();

                if (booksList.Any())
                {
                    model.BookList = booksList;
                }
                else
                {
                    model.BookList = new List<Book>();
                }

            }
            return View(model);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_categoryServices.GetAll(), "CategoryID", "Name");
            ViewData["SupplierID"] = new SelectList(_supplierServices.GetAll(), "SupplierID", "Name");
           
            return View();
        }

        // POST: Books/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Price,Description,ImageUrl,AvailableQuantity,IsBookOfTheWeek,SupplierID,CategoryID")] Book book, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0) // Không null không rỗng
            {
                // Thực hiện trỏ tới thư mục root để lát thực hiện việc copy
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", imageFile.FileName); // Bước 2
                // Kết quả: aaa/wwwroot/images/xxx.jpg
                var stream = new FileStream(path, FileMode.Create);
                // Vì chúng ta thực hiện việc copy => Tạo mới => Create
                imageFile.CopyTo(stream); // Copy ảnh chọn ở form vào wwwroot/images
                // Gán lại giá trị cho thuộc tính Description => Bước 3
                book.ImageUrl = imageFile.FileName; // Bước 4
            }
            if (ModelState.IsValid)
            {
                _booksServices.Add(book);
                await _booksServices.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();
            ViewData["CategoryID"] = new SelectList(_categoryServices.GetAll(), "CategoryID", "Name", book.CategoryID);
            ViewData["SupplierID"] = new SelectList(_supplierServices.GetAll(), "SupplierID", "Name", book.SupplierID);
            
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _booksServices.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_categoryServices.GetAll(), "CategoryID", "Name", book.CategoryID);
            ViewData["SupplierID"] = new SelectList(_supplierServices.GetAll(), "SupplierID", "Name", book.SupplierID);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Price,Description,ImageUrl,AvailableQuantity,IsBookOfTheWeek,SupplierID,CategoryID")] Book book, IFormFile imageFile)
        {
            if (id != book.ID)
            {
                return NotFound();
            }
            if (imageFile != null && imageFile.Length > 0) // Không null không rỗng
            {
                // Thực hiện trỏ tới thư mục root để lát thực hiện việc copy
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", imageFile.FileName); // Bước 2
                // Kết quả: aaa/wwwroot/images/xxx.jpg
                var stream = new FileStream(path, FileMode.Create);
                // Vì chúng ta thực hiện việc copy => Tạo mới => Create
                imageFile.CopyTo(stream); // Copy ảnh chọn ở form vào wwwroot/images
                // Gán lại giá trị cho thuộc tính Description => Bước 3
                book.ImageUrl = imageFile.FileName; // Bước 4
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _booksServices.Update(book);
                    await _booksServices.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            ViewData["CategoryID"] = new SelectList(_categoryServices.GetAll(), "CategoryID", "Name", book.CategoryID);
            ViewData["SupplierID"] = new SelectList(_supplierServices.GetAll(), "SupplierID", "Name", book.SupplierID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _booksServices.GetByIdIncludedAsync(id);

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var books = await _booksServices.GetByIdAsync(id);
            _booksServices.Remove(books);
            await _booksServices.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BookExists(Guid id)
        {
            return _booksServices.Exists(id);
        }
    }
}
