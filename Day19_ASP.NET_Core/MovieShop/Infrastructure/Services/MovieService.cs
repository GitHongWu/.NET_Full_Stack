using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.GetHighestRevenueMovies();

            var movieCardList = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCardList.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                    Title = movie.Title
                });
            }

            return movieCardList;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetailsById(int id)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) throw new NotFoundException("Movie", id);

            List<GenreResponseModel> genres = new List<GenreResponseModel>();
            foreach (var g in movie.Genres)
            {
                genres.Add(new GenreResponseModel
                {
                    Id = g.Id,
                    Name = g.Name,
                });
            }

            List<CastResponseModel> casts = new List<CastResponseModel>();
            foreach (var mc in movie.MovieCasts)
            {
                casts.Add(new CastResponseModel
                {
                    Id = mc.Cast.Id,
                    Name = mc.Cast.Name,
                    Gender = mc.Cast.Gender,
                    TmdbUrl = mc.Cast.TmdbUrl,
                    ProfilePath = mc.Cast.ProfilePath,
                    Character = mc.Character,
                });
            }

            var response = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Rating = movie.Rating,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                RunTime = movie.RunTime,
                Price = movie.Price,
                ReleaseDate = movie.ReleaseDate,
                Genres = genres,
                Casts = casts,
            };
            return response;
        }
    }
}
