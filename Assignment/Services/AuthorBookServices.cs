using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class AuthorBookServices : IAuthorBookServices
    {

        private readonly ShopContext _shopContext;

        public AuthorBookServices(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public IEnumerable<AuthorBooks> AuthorBooks => _shopContext.AuthorBooks.Include(x=>x.Books).Include(x=>x.Author);

        public void Add(AuthorBooks authorBooks)
        {
            _shopContext.AuthorBooks.Add(authorBooks);
        }

        public bool Exists(Guid id)
        {
            return _shopContext.AuthorBooks.Any(p => p.Id == id);
        }

        public IEnumerable<AuthorBooks> GetAll()
        {
            return _shopContext.AuthorBooks.ToList();
        }

        public async Task<IEnumerable<AuthorBooks>> GetAllAsync()
        {
            return await _shopContext.AuthorBooks.ToListAsync();
        }

        public AuthorBooks GetById(Guid? id)
        {
            return _shopContext.AuthorBooks.FirstOrDefault(x=>x.Id == id);
        }

        public async Task<AuthorBooks> GetByIdAsync(Guid? id)
        {
            return await _shopContext.AuthorBooks.FirstOrDefaultAsync(x => x.Id == id);

        }

        public void Remove(AuthorBooks authorBooks)
        {
            _shopContext.AuthorBooks.Remove(authorBooks);
        }

        public void SaveChanges()
        {
            _shopContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _shopContext.SaveChangesAsync();
        } 

        public void Update(AuthorBooks authorBooks)
        {
            _shopContext.AuthorBooks.Update(authorBooks);
        }
    }
}
