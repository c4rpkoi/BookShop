using Assignment.Models;

namespace Assignment.IServices
{
    public interface ISupplierServices
    {
        IEnumerable<Supplier> Suppliers { get; }

        Supplier GetById(Guid? id);
        Task<Supplier> GetByIdAsync(Guid? id);

        bool Exists(Guid id);

        IEnumerable<Supplier> GetAll();
        Task<IEnumerable<Supplier>> GetAllAsync();

        void Add(Supplier supplier);
        void Update(Supplier supplier);
        void Remove(Supplier supplier);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
