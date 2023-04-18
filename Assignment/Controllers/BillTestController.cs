using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Assignment.Controllers
{
    public class BillTestController : Controller
    {
        private readonly ShopContext _context;

        public BillTestController(ShopContext context)
        {
            _context = context;
        }



        // GET: BillTest
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bills.ToListAsync());
        }

        // GET: Bill/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .SingleOrDefaultAsync(m => m.BillID == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bill/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bill/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillId,FirstName,LastName,AddressLine,PhoneNumber,Email,Status")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                bill.Status = 0;
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bill);
        }

        // GET: Bill/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Bills.SingleOrDefaultAsync(m => m.BillID == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Bill/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BillId,FirstName,LastName,AddressLine,PhoneNumber,Email,Status")] Bill bill)
        {
            if (id != bill.BillID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.BillID))
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
            return View(bill);
        }

        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Bills
                .SingleOrDefaultAsync(m => m.BillID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Bills.SingleOrDefaultAsync(m => m.BillID == id);
            _context.Bills.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BillExists(Guid id)
        {
            return _context.Bills.Any(e => e.BillID == id);
        }
    }
}
