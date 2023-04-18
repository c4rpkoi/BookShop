using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ShopContext _shopContext;

        public CategoryServices(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public IEnumerable<Category> Categories => _shopContext.Categories.Include(x=>x.Books);

        public void Add(Category category)
        {
            _shopContext.Categories.Add(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _shopContext.Categories.ToList();
        }


        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _shopContext.Categories.ToListAsync();
        }

        public Category GetById(Guid? id)
        {
            return _shopContext.Categories.FirstOrDefault(p => p.CategoryID == id);
        }

        public async Task<Category> GetByIdAsync(Guid? id)
        {
            return await _shopContext.Categories.FirstOrDefaultAsync(p => p.CategoryID == id); 
        }
        public bool Exists(Guid id)
        {
            return _shopContext.Books.Any(p => p.ID == id);
        }

        public void Remove(Category category)
        {
            _shopContext.Remove(category);
        }

        public void SaveChanges()
        {
            _shopContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _shopContext.SaveChangesAsync();
        }

        public void Update(Category category)
        {
            _shopContext.Update(category);
        }
    }
}
