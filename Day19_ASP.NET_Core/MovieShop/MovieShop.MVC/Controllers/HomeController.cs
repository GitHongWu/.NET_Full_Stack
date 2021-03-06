using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;

namespace MovieShop.MVC.Controllers
{
    //public class Movie
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //}
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly IMovieService _movieService;

        // Constructor Injection
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            // we need to go to database and display top revenue services
            // thin controllers...

            //MovieService service = new MovieService();
            //var movies = service.GetTopRevenueMovies();

            // send data to view so that that view display the top movie
            // 1. passing the data from my controller to my view using strongly typed Models
            // 2. ViewBag
            // ViewBag.PageTitle = "Top Revenue Movies";
            // ViewBag.MoviesCount = movies.Count;
            // 3. ViewData
            // ViewData["MyCustomData"] = "Some Information";

            var movies = await _movieService.GetTopRevenueMovies();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TopMovies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
