using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index(int id = 0)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }
    }
}
