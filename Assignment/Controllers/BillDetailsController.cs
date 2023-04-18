using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class BillDetailsController : Controller
    {
        // GET: BillDetails
        public ActionResult Index()
        {
            return View();
        }

        // GET: BillDetails/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: BillDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: BillDetails/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: BillDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
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

        // GET: BillDetails/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: BillDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
