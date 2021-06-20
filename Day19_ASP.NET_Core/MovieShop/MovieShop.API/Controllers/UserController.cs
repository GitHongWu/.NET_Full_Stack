using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        public UserController(IMovieService movieService, IUserService userService, ICurrentUserService currentUserService)
        {
            _movieService = movieService;
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpPost("purchase")]
        public async Task<ActionResult> CreatePurchase([FromBody] PurchaseRequestModel model)
        {
            await _userService.PurchaseMovie(model);
            return Ok();
        }

        //localhost:4
        [Authorize]
        [HttpGet("{id:int}/purchases")]
        public async Task<ActionResult> GetUserPurchasedMoviesAsync(int id)
        {
            if (_currentUserService.UserId != id)
            {
                return Unauthorized("please send correct id");
            }
            // get userid from token and compare with id that is passed in the URL, then if they equal call the service
            // get all movies purchased by user id
            // we need to check if the client who is calling this method is send a valid jwt
            var userPurchasedMovies = await _userService.GetAllPurchasesForUser(id);
            return Ok(userPurchasedMovies);
        }
    }
}
