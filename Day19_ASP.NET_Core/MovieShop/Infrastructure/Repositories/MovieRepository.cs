using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Rating).Take(6).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(6).ToListAsync();
            return movies;
        }


        public override async Task<Movie> GetById(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return null;
                //throw new NotFoundException("Movie Not found");
            }

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movie.Rating = movieRating;

            //// LINQ to get Casts data
            //var casts_data = (from c in _dbContext.Casts 
            //                  join mc in _dbContext.MovieCasts on c.Id equals mc.CastId 
            //                  join m in _dbContext.Movies on mc.MovieId equals m.Id
            //                  select new { mc.Cast, mc.Character, m.Id }).Where(m => m.Id == id).Distinct();

            //ICollection<MovieCast> casts = new List<MovieCast>();
            //foreach (var c in casts_data)
            //{
            //    casts.Add(new MovieCast
            //    {
            //        Cast = c.Cast,
            //        Character = c.Character,
            //    });
            //}
            //movie.MovieCasts = casts;

            //// LINQ to get Genre data
            //var genres_data = (from g in _dbContext.Genres
            //                   join mg in _dbContext.MovieGenres on g.Id equals mg.genreid
            //                   select new { g.id, g.name, mg.movieid }).where(m => m.movieid == id).distinct();

            //ICollection<Genre> genres = new List<Genre>();
            //foreach (var g in genres_data)
            //{
            //    genres.Add(new Genre
            //    {
            //        Id = g.Id,
            //        Name = g.Name,
            //    });
            //}
            //movie.Genres = genres;

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesInOrder()
        {
            var movies = await _dbContext.Movies.OrderBy(m => m.Id).Take(6).ToListAsync();
            return movies;
        }
    }
}
