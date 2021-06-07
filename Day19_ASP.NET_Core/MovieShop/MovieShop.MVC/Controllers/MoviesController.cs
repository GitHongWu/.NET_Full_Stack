using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models.Response;
using ApplicationCore.ServiceInterfaces;


namespace MovieShop.MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService service)
        {
            _movieService = service;
        }

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
