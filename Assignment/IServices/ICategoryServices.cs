using Assignment.Models;

namespace Assignment.IServices
{
    public interface ICategoryServices
    {
        IEnumerable<Category> Categories { get; }

        Category GetById(Guid? id);
        Task<Category> GetByIdAsync(Guid? id);

        bool Exists(Guid id);

        IEnumerable<Category> GetAll();
        Task<IEnumerable<Category>> GetAllAsync();

        void Add(Category category);
        void Update(Category category);
        void Remove(Category category);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
