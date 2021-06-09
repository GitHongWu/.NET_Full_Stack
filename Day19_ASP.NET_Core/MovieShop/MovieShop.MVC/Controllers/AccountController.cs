using ApplicationCore.Models.Request;
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
        public IActionResult Register()
        {
            //show a view with empty boxes for name, passward, email
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterRequestModel model)
        {
            //check for model  validation on server also
            if (ModelState.IsValid)
            {
                // save to database
            }
            return View();
        }

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
