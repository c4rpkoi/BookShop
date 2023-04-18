using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Assignment.Services
{
    public class BooksServices : IBooksServices
    {
        private readonly ShopContext _shopContext;

        public BooksServices(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public IEnumerable<Book> Books => _shopContext.Books.Include(p=>p.Category).Include(p=>p.Supplier).Include(p=>p.AuthorBooks);

        public IEnumerable<Book> BooksOfTheWeek => _shopContext.Books.Where(p=>p.IsBookOfTheWeek).Include(p=>p.Category);

        public void Add(Book book)
        {
            _shopContext.Add(book);
        }

        public bool Exists(Guid id)
        {
            return _shopContext.Books.Any(p=>p.ID == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _shopContext.Books.ToList();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _shopContext.Books.ToListAsync();
        }

        public IEnumerable<Book> GetAllIncluded()
        {
            return _shopContext.Books.Include(p => p.Category).Include(p => p.Supplier).Include(p => p.AuthorBooks).ToList();
        }

        public async Task<IEnumerable<Book>> GetAllIncludedAsync()
        {
            return await _shopContext.Books.Include(p => p.Category).Include(p => p.Supplier).Include(p => p.AuthorBooks).ToListAsync();
        }

        public Book GetById(Guid? id)
        {
            return _shopContext.Books.FirstOrDefault(p => p.ID == id);
        }

        public async Task<Book> GetByIdAsync(Guid? id)
        {
            return await _shopContext.Books.FirstOrDefaultAsync(p => p.ID == id);
        }

        public Book GetByIdIncluded(Guid? id)
        {
            return _shopContext.Books.Include(p => p.Category).Include(p => p.Supplier).Include(p => p.AuthorBooks).FirstOrDefault(p=>p.ID==id);
        }

        public async Task<Book> GetByIdIncludedAsync(Guid? id)
        {
            return await _shopContext.Books.Include(p => p.Category).Include(p => p.Supplier).Include(p => p.AuthorBooks).FirstOrDefaultAsync(p => p.ID == id);
        }

        public void Remove(Book book)
        {
            _shopContext.Remove(book);
        }

        public void SaveChanges()
        {
            _shopContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _shopContext.SaveChangesAsync();

        }

        public void Update(Book book)
        {
            _shopContext.Update(book);
        }
    }
}
