using Assignment.Models;

namespace Assignment.IServices
{
    public interface IBooksServices
    {
        IEnumerable<Book> Books { get; }
        IEnumerable<Book> BooksOfTheWeek { get; }

        Book GetById(Guid? id);
        Task<Book> GetByIdAsync(Guid? id);

        Book GetByIdIncluded(Guid? id);
        Task<Book> GetByIdIncludedAsync(Guid? id);

        bool Exists(Guid id);

        IEnumerable<Book> GetAll();
        Task<IEnumerable<Book>> GetAllAsync();

        IEnumerable<Book> GetAllIncluded();
        Task<IEnumerable<Book>> GetAllIncludedAsync();

        void Add(Book book);
        void Update(Book book);
        void Remove(Book book);

        void SaveChanges();
        Task SaveChangesAsync();


    }
}
