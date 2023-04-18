using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class SupplierServices : ISupplierServices
    {
        private readonly ShopContext _shopContext;

        public SupplierServices(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public IEnumerable<Supplier> Suppliers => _shopContext.Suppliers.Include(x=>x.Book);

        public void Add(Supplier supplier)
        {
            _shopContext.Suppliers.Add(supplier);
        }

        public bool Exists(Guid id)
        {
            return _shopContext.Suppliers.Any(p => p.SupplierID == id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _shopContext.Suppliers.ToList();
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _shopContext.Suppliers.ToListAsync();

        }

        public Supplier GetById(Guid? id)
        {
            return _shopContext.Suppliers.FirstOrDefault(p => p.SupplierID == id);
        }

        public async Task<Supplier> GetByIdAsync(Guid? id)
        {
            return await _shopContext.Suppliers.FirstOrDefaultAsync(p => p.SupplierID == id);
        }

        public void Remove(Supplier supplier)
        {
            _shopContext.Remove(supplier);
        }

        public void SaveChanges()
        {
            _shopContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _shopContext.SaveChangesAsync();
        }

        public void Update(Supplier supplier)
        {
            _shopContext.Update(supplier);
        }
    }
}
