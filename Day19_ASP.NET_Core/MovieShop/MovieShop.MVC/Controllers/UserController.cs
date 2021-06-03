using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Purchase()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Favorite()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Unfavorite()
        {
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult Favorite()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Review()
        {
            return RedirectToAction("Index");
        }

        //[HttpPut]
        //public IActionResult Review()
        //{
        //    return RedirectToAction("Index");
        //}

        [HttpDelete]
        public IActionResult Delete(int userId, int movieId)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Purchase(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Favorite(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reviews(int id)
        {
            return View();
        }
    }
}
