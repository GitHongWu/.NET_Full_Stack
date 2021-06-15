using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        //method for getting top 30 highest movies
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();
        Task<List<MovieCardResponseModel>> GetTopRatedMovies();
        Task<MovieDetailsResponseModel> GetMovieDetailsById(int id);
        Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequestModel model);
        Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequestModel model);
        Task<List<MovieCardResponseModel>> GetAllMovies();
        Task<List<MovieCardResponseModel>> GetAllMoviePurchases();

    }
}
