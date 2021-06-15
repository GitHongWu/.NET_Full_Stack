using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using System.Net;
using static ApplicationCore.Models.Response.PurchaseResponseModel;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieService _movieService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPurchaseRepository _purchaseRepository;

        public UserService(IUserRepository userRepository, IMovieService movieService, ICurrentUserService currentUserService, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _movieService = movieService;
            _currentUserService = currentUserService;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel userRegisterRequestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(userRegisterRequestModel.Email);

            if(dbUser != null)
            {
                throw new ConflictException("User already exists, please try to login");
            }

            // generate unique SALT
            var salt = CreateSalt();

            // hash the password with userRegisterRequestModel.Password + salt from above step
            var hashedPassword = CreateHashedPassword(userRegisterRequestModel.Password, salt);

            // call user repository to save the user info
            var user = new User
            {
                FirstName = userRegisterRequestModel.FirstName,
                LastName = userRegisterRequestModel.LastName,
                Email = userRegisterRequestModel.Email,
                DateOfBirth = userRegisterRequestModel.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };
            var createdUser = await _userRepository.Add(user);

            // convert the returned user entity to UserRegisterResponseModel
            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email
            };

            return response;
        }

        public async Task<UserLoginResponseModel> Login(string email, string password)
        {
            // go to database and get the user info -- row by email
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                // return null
                return null;
            }

            // get the password from UI and salt from above step from database and call CreateHashedPassword method

            var hashedPassword = CreateHashedPassword(password, user.Salt);

            if (hashedPassword == user.HashedPassword)
            {
                // user entered correct password

                var loginResponseModel = new UserLoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
                var roles = user.Roles;
                if (roles != null && roles.Any())
                    loginResponseModel.Roles = roles.Select(r => r.Name).ToList();
                return loginResponseModel;
            }

            return null;
        }

        public async Task<UserProfileResponseModel> EditUserProfile(UserProfileRequestModel userProfileRequestModel, int id)
        {
            var dbUser = await _userRepository.GetUserById(id);

            if (dbUser == null)
            {
                throw new Exception("User does not exist");
            }

            dbUser.FirstName = userProfileRequestModel.FirstName;
            dbUser.LastName = userProfileRequestModel.LastName;
            dbUser.Email = userProfileRequestModel.Email;

            var updatedUser = await _userRepository.Update(dbUser);

            var response = new UserProfileResponseModel
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                FullName = updatedUser.FirstName + " " + updatedUser.LastName,
            };
            return response;
        }

        private string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string CreateHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task PurchaseMovie(PurchaseRequestModel model)
        {
            if (_currentUserService.UserId != model.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to purchase");

            model.UserId = _currentUserService.UserId;

            // See if Movie is already purchased.
            if (await IsMoviePurchased(model))
                throw new ConflictException("Movie already Purchased");
            // Get Movie Price from Movie Table
            var movie = await _movieService.GetMovieDetailsById(model.MovieId);
            model.TotalPrice = movie.Price;


            //var purchase = _mapper.Map<Purchase>(purchaseRequest);
            var purchase = new Purchase
            {
                UserId = model.UserId,
                PurchaseNumber = model.PurchaseNumber,
                TotalPrice = model.TotalPrice,
                PurchaseDateTime = model.PurchaseDateTime,
                MovieId = model.MovieId,
            };
            await _purchaseRepository.Add(purchase);
        }

        private async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            return await _purchaseRepository.GetExists(p =>
                p.UserId == purchaseRequest.UserId && p.MovieId == purchaseRequest.MovieId);
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            if (_currentUserService.UserId != id)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Purchases");

            //var purchasedMovies = await _purchaseRepository.ListAllWithIncludesAsync(
            //    p => p.UserId == _currentUserService.UserId,
            //    p => p.Movie);

            var purchasedMovies = await _purchaseRepository.GetAllPurchasesByUser(id);

            //return _mapper.Map<PurchaseResponseModel>(purchasedMovies);
            List<PurchasedMovieResponseModel> moviesList = new List<PurchasedMovieResponseModel>();
            foreach (var item in purchasedMovies)
            {
                moviesList.Add(new PurchasedMovieResponseModel 
                {
                    Id = item.MovieId,
                    Title = item.Movie.Title,
                    PosterUrl = item.Movie.PosterUrl,
                    ReleaseDate = item.Movie.ReleaseDate,
                    PurchaseDateTime = item.PurchaseDateTime,
                });
            }
            
            var response = new PurchaseResponseModel
            {
                UserId = id,
                PurchasedMovies = moviesList,
            };
            return response;
        }

        public async Task<User> GetUser(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) 
                throw new NotFoundException("User", id);

            //var response = _mapper.Map<UserRegisterResponseModel>(user);
            var response = new UserRegisterResponseModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
            return response;
        }
    }
}
