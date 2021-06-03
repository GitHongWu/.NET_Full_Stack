using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.Controllers
{
    public class MoviesController : Controller
    {
        [HttpGet]
        public IActionResult Index(int id = 0)
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Index(int id)
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult TopRated()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TopRevenue()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Genre(int id)
        {
            return View();
        }
    }
}
