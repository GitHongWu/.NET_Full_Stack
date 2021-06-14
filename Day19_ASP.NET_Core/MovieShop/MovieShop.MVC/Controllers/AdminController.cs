using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMovieService _movieService;

        public AdminController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult CreateMovie()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(MovieCreateRequestModel model)
        {
            //check for model validation on server also
            if (ModelState.IsValid)
            {
                // save to database
                var movie = await _movieService.CreateMovie(model);
                // redirect to Login
                return LocalRedirect(localUrl: $"~/Movies/Details/{movie.Id}");
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCast()
        {
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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
