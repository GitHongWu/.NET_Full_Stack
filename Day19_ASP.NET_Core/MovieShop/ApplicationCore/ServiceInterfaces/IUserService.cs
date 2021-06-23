using ApplicationCore.Entities;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel userRegisterRequestModel);
        Task<UserLoginResponseModel> Login(string email, string password);
        Task<UserProfileResponseModel> EditUserProfile(UserProfileRequestModel userProfileRequestModel, int Id);
        Task PurchaseMovie(PurchaseRequestModel purchaseRequest);
        Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);
        Task<User> GetUser(string email);
        Task<UserRegisterResponseModel> GetUserDetails(int id);
        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(int id, int movieId);
        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task DeleteMovieReview(int userId, int movieId);
        Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);
        Task<ReviewResponseModel> GetAllReviewsByUser(int id);

        // delete
        // EditUser
        // Change Password
        // Purchase Movie
        // Favorite Movie
        // Add Review
        // Get All Purchased Movies
        // Get All Favorited Movies
        // Edit Review
        // Remove Favorite
        // Get User Details
        // 

    }
}
