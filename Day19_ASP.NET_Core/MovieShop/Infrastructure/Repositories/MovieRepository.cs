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
            //var movies = await _dbContext.Movies.Include(m => m.Reviews).AverageAsync(r => r == null ? 0 : r.Rating).OrderByDescending(m => m.Rating).Take(25).ToListAsync();


            //var movies = await _dbContext.Movies.ToListAsync();
            //foreach (var movie in movies)
            //{
            //    movie.Rating = await _dbContext.Reviews.Where(r => r.MovieId == movie.Id).DefaultIfEmpty()
            //    .AverageAsync(r => r == null ? 0 : r.Rating);
            //}
            //movies.OrderByDescending(m => m.Rating).Take(25);
            //return movies;


            //var movie_rating = (from m in _dbContext.Movies
            //                    join r in _dbContext.Reviews on m.Id equals r.MovieId
            //                    select new { m.Id, m.PosterUrl, m.ReleaseDate, m.Title, r.Rating })
            //                    .GroupBy(m => new { m.Id, m.PosterUrl, m.ReleaseDate, m.Title })
            //                    .Select(g => new { m = g.Key, Rating = g.Average(x => x.Rating) })
            //                    .OrderByDescending(m => m.Rating).Take(25).ToListAsync();
            //return movie_rating;


            //return await _dbContext.Movies
            //           .Join(_dbContext.Reviews, m => m.Id, r => r.MovieId,
            //                 (m, r) => new { m.Id, m.PosterUrl, m.ReleaseDate, m.Title, r.Rating })
            //           .GroupBy(m => new { m.Id, m.PosterUrl, m.ReleaseDate, m.Title },
            //                    pair => new { pair.Id, pair.PosterUrl, pair.ReleaseDate, pair.Title }, // Key selector
            //                    pair => pair.Rating, // Element selector
            //                    (key, numbers) => new {
            //                        movie = key,
            //                        Rating = numbers.Average()
            //                    });

            //return _dbContext.Movies.Include(m => m.Reviews).GroupBy(m => new { m.Id, m.PosterUrl, m.ReleaseDate, m.Title }).Select(g => new { m = g.Key, Rating = g.Average(x => x.Rating) }).OrderByDescending(m => m.Rating).Take(25).ToListAsync();

            var topRatedMovies = await _dbContext.Reviews.Include(m => m.Movie)
                .GroupBy(r => new { Id = r.MovieId, r.Movie.PosterUrl, r.Movie.Title, r.Movie.ReleaseDate})
                .OrderByDescending(g => g.Average(m => m.Rating))
                .Select(m => new Movie { Id = m.Key.Id, PosterUrl = m.Key.PosterUrl, Title = m.Key.Title, ReleaseDate = m.Key.ReleaseDate, Rating = m.Average(x => x.Rating)})
                .Take(25).ToListAsync();
            return topRatedMovies;
        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(25).ToListAsync();
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
            var movies = await _dbContext.Movies.Take(25).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            var totalMoviesCountByGenre =
                await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == genreId).SelectMany(g => g.Movies)
                    .CountAsync();

            if (totalMoviesCountByGenre == 0)
            {
                throw new NotFoundException("NO Movies found for this genre");
            }
            var movies = await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == genreId)
                .SelectMany(g => g.Movies)
                .OrderByDescending(m => m.Revenue)
                .Take(25).ToListAsync();

            return movies;
        }
    }
}
