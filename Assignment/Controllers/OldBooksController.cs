﻿using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class OldBooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
