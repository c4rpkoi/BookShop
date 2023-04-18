using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class AuthorServices : IAuthorServices
    {
        private readonly ShopContext _shopContext;

        public AuthorServices(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public IEnumerable<Author> Authors => _shopContext.Authors.Include(x=>x.AuthorBooks);

        public void Add(Author author)
        {
            _shopContext.Authors.Add(author);
        }

        public bool Exists(Guid id)
        {
            return _shopContext.Authors.Any(p => p.AuthorID == id);
        }

        public IEnumerable<Author> GetAll()
        {
            return _shopContext.Authors.ToList();

        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _shopContext.Authors.ToListAsync();

        }

        public Author GetById(Guid? id)
        {
            return _shopContext.Authors.FirstOrDefault(p => p.AuthorID == id);

        }

        public async Task<Author> GetByIdAsync(Guid? id)
        {
            return await _shopContext.Authors.FirstOrDefaultAsync(p => p.AuthorID == id);

        }

        public void Remove(Author author)
        {
            _shopContext.Remove(author);

        }

        public void SaveChanges()
        {
            _shopContext.SaveChanges();

        }

        public async Task SaveChangesAsync()
        {
            await _shopContext.SaveChangesAsync();
        }

        public void Update(Author author)
        {
            _shopContext.Update(author);
        }
    }
}
