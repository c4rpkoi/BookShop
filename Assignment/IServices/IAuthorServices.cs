using Assignment.Models;

namespace Assignment.IServices
{
    public interface IAuthorServices
    {
        IEnumerable<Author> Authors { get; }

        Author GetById(Guid? id);
        Task<Author> GetByIdAsync(Guid? id);

        bool Exists(Guid id);

        IEnumerable<Author> GetAll();
        Task<IEnumerable<Author>> GetAllAsync();

        void Add(Author author);
        void Update(Author author);
        void Remove(Author author);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
