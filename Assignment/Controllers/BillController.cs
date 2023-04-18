using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using Assignment.IServices;
using Assignment.Models;
using Assignment.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    [Authorize]
    public class BillController : Controller
    {

        private readonly IBillServices _billServices;
        private readonly Cart _cart;
        private readonly ShopContext _shopContext;
        private readonly UserManager<IdentityUser> _userManager;

        public BillController(IBillServices billServices, Cart cart, ShopContext shopContext, UserManager<IdentityUser> userManager)
        {
            _billServices = billServices;
            _cart = cart;
            _shopContext = shopContext;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Checkout()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Checkout(Bill bill)
        {
            
            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId!=null)
            {
                
            bill.UserId = userId;
            }

            var items = await _cart.GetCartDetailsAsync();
            _cart.CartDetails = items;
            foreach (var item in items)
            {
            var book = _shopContext.Books.FirstOrDefault(c => c.ID == item.BookID);

            if (book.AvailableQuantity< item.Amount)
            {
                
                ModelState.AddModelError("", "Số lượng sản phẩm tồn không đủ");
            }
            }

            if (_cart.CartDetails.Count == 0)
            {
                ModelState.AddModelError("", "Giỏ hàng trống");
            }

            if (ModelState.IsValid)
            {
                await _billServices.CreateBillAsync(bill);
                await _cart.ClearCartAsync();

                return RedirectToAction("CheckoutComplete");
            }
            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();

            return View(bill);
        }

        [AllowAnonymous]
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = $"Hàng đến sớm thôi";
            return View();
        }

        // GET: Reviews
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                var allBills = await _shopContext.Bills.Include(o => o.BillLines).Include(o => o.User).ToListAsync();
                return View(allBills);
            }
            else
            {
                var bills = await _shopContext.Bills.Include(o => o.BillLines).Include(o => o.User)
                    .Where(r => r.User == user).ToListAsync();
                return View(bills);
            }
        }

        // GET: Bill/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bills = await _shopContext.Bills.Include(o => o.BillLines).Include(o => o.User)
                .SingleOrDefaultAsync(m => m.BillID == id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userRoles = await _userManager.GetRolesAsync(user);
            bool isAdmin = userRoles.Any(r => r == "Admin");

            if (bills == null)
            {
                return NotFound();
            }

            if (isAdmin == false)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                if (bills.UserId != userId)
                {
                    return BadRequest("Quyền gì mà đòi xem chuyên đề hả anh zai");
                }
            }

            var billDetailsList = _shopContext.BillDetails.Include(o => o.Book).Include(o => o.Bill)
                .Where(x => x.BillID == bills.BillID);

            ViewBag.BillDetailsList = billDetailsList;

            return View(bills);
        }

        public IActionResult Done(Guid id)
        {
            var x = _shopContext.Bills.FirstOrDefault(c => c.BillID == id);
            x.Status = 1;
            _shopContext.Update(x);
            _shopContext.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Bills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bill/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bill/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bill/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _shopContext.Bills.Include(o => o.User)
                .SingleOrDefaultAsync(m => m.BillID == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bill = await _shopContext.Bills.SingleOrDefaultAsync(m => m.BillID == id);
            _shopContext.Bills.Remove(bill);
            await _shopContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
