using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SupplierController : Controller
    {
        private readonly ShopContext _shopContext;
        private readonly ISupplierServices _supplierServices;

        public SupplierController(ShopContext shopContext, ISupplierServices supplierServices)
        {
            _shopContext = shopContext;
            _supplierServices = supplierServices;
        }



        //GET: Supplier
        public async Task<IActionResult> Index()
        {
            return View(await _supplierServices.GetAllAsync());
        }

        //GET: Supplier/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierServices.GetByIdAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        //Get: Supplier/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierID,Name,Description")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _supplierServices.Add(supplier);
                await _supplierServices.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        //GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _supplierServices.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        //POST:Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SupplierID,Name,Description")] Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _supplierServices.Update(supplier);
                    await _supplierServices.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(supplier.SupplierID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        //GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierServices.GetByIdAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        //POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComfirmed(Guid id)
        {
            var supplier = await _supplierServices.GetByIdAsync(id);
            _supplierServices.Remove(supplier);
            await _supplierServices.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool CategoryExists(Guid id)
        {
            return _supplierServices.Exists(id);
        }
    }
}
