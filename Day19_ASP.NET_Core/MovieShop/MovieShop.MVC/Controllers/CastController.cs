using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class CastController : Controller
    {
        public IActionResult Cast(int id)
        {
            return View();
        }
    }
}
