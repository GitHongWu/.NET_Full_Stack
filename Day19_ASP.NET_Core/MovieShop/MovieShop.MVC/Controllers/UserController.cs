using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public UserController(ICurrentUserService currentUserService, IMovieService movieService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _movieService = movieService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserPurchasedMovies()
        {
            var userId = _currentUserService.UserId;
            // get the user id
            //
            // make a request to the database and get info from Purchase Table 
            // select * from Purchase where userid = @getfromcookie
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PurchaseMovie(int id)
        {
            var movie = await _movieService.GetMovieDetailsById(id);
            return View(movie);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PurchaseMovie(PurchaseRequestModel model)
        {
            // check if user already logged in
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    // get userid from CurrentUser and create a row in Purchase Table
                    await _userService.PurchaseMovie(model);
                    return Ok();
                }
            }
            return View();
        }

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
