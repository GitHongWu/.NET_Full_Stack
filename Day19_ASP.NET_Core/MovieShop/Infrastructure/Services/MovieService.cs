using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ICurrentUserService _currentUserService;

        public MovieService(IMovieRepository movieRepository, ICurrentUserService currentUserService)
        {
            _movieRepository = movieRepository;
            _currentUserService = currentUserService;
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

        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequestModel model)
        {
            if(!_currentUserService.IsAdmin)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to create movie");

            var dbMovie = await _movieRepository.GetById(model.Id);

            if (dbMovie != null)
            {
                throw new ConflictException("Movie already exists");
            }

            //ICollection<Genre> genres = new List<Genre>();
            //foreach (var g in model.Genres)
            //{
            //    genres.Add(new Genre
            //    {
            //        Id = g.Id,
            //        Name = g.Name,
            //    });
            //}
            var movie = new Movie
            {
                Id = model.Id,
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Revenue = model.Revenue,
                Budget = model.Budget,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                OriginalLanguage = model.OriginalLanguage,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                //Genres = genres,
            };
            var createdMovie = _movieRepository.Add(movie);

            var response = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Revenue = movie.Revenue,
                Budget = movie.Budget,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
                //Genres = model.Genres,
            };

            return response;
        }
    }
}
