using ApplicationCore.ServiceInterfaces;
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
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET /api​/Movies
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            if (movies.Any())
            {
                return Ok(movies);
            }
            return NotFound("no movies found");
        }

        // GET /api​/Movies​/{id}
        [HttpGet]
        [Route("{id:int}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetailsById(id);
            if (movie != null)
            {
                return Ok(movie);
            }
            return NotFound("no movies found");
        }

        // GET /api​/Movies​/toprated
        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();

            if (movies.Any())
            {
                return Ok(movies);
            }
            return NotFound("no movies found");
        }

        // GET /api​/Movies​/toprevenue
        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetHighestGrossingMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            if (movies.Any())
            {
                return Ok(movies);
            }
            return NotFound("no movies found");
        }
    }
}
