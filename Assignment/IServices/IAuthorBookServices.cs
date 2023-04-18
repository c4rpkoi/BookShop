using Assignment.Models;

namespace Assignment.IServices
{
    public interface IAuthorBookServices
    {
        IEnumerable<AuthorBooks> AuthorBooks { get; }

        AuthorBooks GetById(Guid? id);
        Task<AuthorBooks> GetByIdAsync(Guid? id);

        bool Exists(Guid id);

        IEnumerable<AuthorBooks> GetAll();
        Task<IEnumerable<AuthorBooks>> GetAllAsync();

        void Add(AuthorBooks authorBooks);
        void Update(AuthorBooks authorBooks);
        void Remove(AuthorBooks authorBooks);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
