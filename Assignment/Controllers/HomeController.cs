using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (userName.Length < 6 && password.Length < 6)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}