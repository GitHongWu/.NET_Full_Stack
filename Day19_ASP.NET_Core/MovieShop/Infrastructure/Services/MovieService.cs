using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IAsyncRepository<Review> _reviewRepository;

        public MovieService(IMovieRepository movieRepository, ICurrentUserService currentUserService, IPurchaseRepository purchaseRepository,
            IAsyncRepository<Review> reviewRepository)
        {
            _movieRepository = movieRepository;
            _currentUserService = currentUserService;
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;
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

        public async Task<List<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetTopRatedMovies();

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

        public async Task<List<MovieCardResponseModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId);
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

        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequestModel model)
        {
            if(!_currentUserService.IsAdmin)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to create movie");

           // var dbMovie = await _movieRepository.GetById(model.Id);

            //if (dbMovie != null)
            //{
            //    throw new ConflictException("Movie already exists");
            //}

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
            var createdMovie = await _movieRepository.Add(movie);

            var response = new MovieDetailsResponseModel
            {
                Id = createdMovie.Id,
                Title = createdMovie.Title,
                Overview = createdMovie.Overview,
                Tagline = createdMovie.Tagline,
                Revenue = createdMovie.Revenue,
                Budget = createdMovie.Budget,
                ImdbUrl = createdMovie.ImdbUrl,
                TmdbUrl = createdMovie.TmdbUrl,
                PosterUrl = createdMovie.PosterUrl,
                BackdropUrl = createdMovie.BackdropUrl,
                ReleaseDate = createdMovie.ReleaseDate,
                RunTime = createdMovie.RunTime,
                Price = createdMovie.Price,
                //Genres = model.Genres,
            };

            return response;
        }

        public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequestModel model)
        {
            var movie = new Movie
            {
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
            };
            var movieGenres = new List<Genre>();
            foreach (var genre in model.Genres)
            {
                movieGenres.Add(new Genre
                {
                    Id = genre.Id,
                    Name = genre.Name,
                });
            }
            movie.Genres = movieGenres;

            var updatedMovie = await _movieRepository.Update(movie);
            var response = new MovieDetailsResponseModel
            {
                Id = updatedMovie.Id,
                Title = updatedMovie.Title,
                Overview = updatedMovie.Overview,
                Tagline = updatedMovie.Tagline,
                Revenue = updatedMovie.Revenue,
                Budget = updatedMovie.Budget,
                ImdbUrl = updatedMovie.ImdbUrl,
                TmdbUrl = updatedMovie.TmdbUrl,
                PosterUrl = updatedMovie.PosterUrl,
                BackdropUrl = updatedMovie.BackdropUrl,
                ReleaseDate = updatedMovie.ReleaseDate,
                RunTime = updatedMovie.RunTime,
                Price = updatedMovie.Price,
                //Genres = updatedMovie.Genres,
            };

            return response;
        }

        public async Task<List<MovieCardResponseModel>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMoviesInOrder();

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

        public async Task<List<MovieCardResponseModel>> GetAllMoviePurchases()
        {
            var purchasedMovies = await _purchaseRepository.GetAllPurchasedMovies();

            var movieCardList = new List<MovieCardResponseModel>();
            foreach (var movie in purchasedMovies)
            {
                movieCardList.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    PosterUrl = movie.Movie.PosterUrl,
                    ReleaseDate = movie.Movie.ReleaseDate.GetValueOrDefault(),
                    Title = movie.Movie.Title,
                });
            }

            return movieCardList;
        }

        public async Task<List<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}
