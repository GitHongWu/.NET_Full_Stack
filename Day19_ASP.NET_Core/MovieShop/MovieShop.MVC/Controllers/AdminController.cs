using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class AdminController : Controller
    {
        [HttpPost]
        public IActionResult Movie()
        {
            return View();
        }

        //[HttpPut]
        //public IActionResult Movie()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Purchases()
        {
            return View();
        }
    }
}
