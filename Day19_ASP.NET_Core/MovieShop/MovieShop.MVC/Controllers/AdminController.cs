using ApplicationCore.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult CreateMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMovie(MovieCreateRequestModel model)
        {
            //check for model validation on server also
            if (ModelState.IsValid)
            {
                // save to database
            }
            return View();
        }

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
