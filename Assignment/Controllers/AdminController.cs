using Assignment.Data;
using Assignment.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ShopContext _shopContext;
        private readonly IAdminServices _adminServices;

        public AdminController(ShopContext shopContext, IAdminServices adminServices)
        {
            _shopContext = shopContext;
            _adminServices = adminServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        
    }
}
