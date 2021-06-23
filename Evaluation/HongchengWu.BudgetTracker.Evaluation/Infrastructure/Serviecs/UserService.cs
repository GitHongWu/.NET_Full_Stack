using ApplicationCore.Entities;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Serviecs
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenditureRepository _expenditureRepository;

        public UserService(IUserRepository userRepository, IIncomeRepository incomeRepository, IExpenditureRepository expenditureRepository)
        {
            _userRepository = userRepository;
            _incomeRepository = incomeRepository;
            _expenditureRepository = expenditureRepository;
        }

        public async Task<List<UserProfileResponseModel>> GetAllUsers()
        {
            var users = await _userRepository.ListAll();

            List<UserProfileResponseModel> response = new List<UserProfileResponseModel>();
            foreach (var u in users)
            {
                response.Add(new UserProfileResponseModel 
                {
                    Id = u.Id,
                    Email = u.Email,
                    FullName = u.FullName,
                    JoinedOn = u.JoinedOn,
                });
            }

            return response;
        }

        public async Task<UserProfileResponseModel> GetUserById(int userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null) return null;

            UserProfileResponseModel response = new UserProfileResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                JoinedOn = user.JoinedOn,
            };
            return response;
        }

        public async Task<List<UserDetailResponseModel>> GetUserDetailsById(int userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null) return null;

            var userIncomes = await _incomeRepository.GetIncomesByUserId(userId);
            var userExpenditures = await _expenditureRepository.GetExpendituresByUserId(userId);

            List<UserDetailResponseModel> response = new List<UserDetailResponseModel>();
            foreach (var i in userIncomes)
            {
                response.Add(new UserDetailResponseModel
                {
                    Id = i.Id,
                    Type = "Income",
                    Amount = i.Amount,
                    Description = i.Description,
                    Date = i.IncomeDate,
                    Remarks = i.Remarks,
                });
            }
            foreach (var e in userExpenditures)
            {
                response.Add(new UserDetailResponseModel
                {
                    Id = e.Id,
                    Type = "Expenditure",
                    Amount = e.Amount,
                    Description = e.Description,
                    Date = e.ExpDate,
                    Remarks = e.Remarks,
                });
            }

            response = response.OrderByDescending(o => o.Date).ToList();
            return response;
        }

        public async Task<bool> UserExistByEmail(string email)
        {
            return await _userRepository.GetExists(u => u.Email == email);
        }

        public async Task<UserProfileResponseModel> RegisterUser(RegisterUserRequestModel model)
        {
            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                JoinedOn = DateTime.Now,
            };
            var addedUser = await _userRepository.Add(user);

            var response = new UserProfileResponseModel
            {
                Id = addedUser.Id,
                Email = addedUser.Email,
                FullName = addedUser.FullName,
                JoinedOn = addedUser.JoinedOn,
            };

            return response;
        }

        public async Task<UserProfileResponseModel> UpdateUser(UserUpdateRequestModel model)
        {
            var dbUser = await _userRepository.GetExists(u => u.Email == model.Email);
            if (dbUser == false) return null;

            var existUser = await _userRepository.GetUserByEmail(model.Email);
            existUser.FullName = model.FullName;
            existUser.Password = model.Password;

            var updatedUser = await _userRepository.Update(existUser);
            UserProfileResponseModel response = new UserProfileResponseModel
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                FullName = updatedUser.FullName,
                JoinedOn = updatedUser.JoinedOn,
            };
            return response;
        }

        public async Task<UserProfileResponseModel> DeleteUser(int userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null) return null;

            UserProfileResponseModel response = new UserProfileResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                JoinedOn = user.JoinedOn,
            };

            await _userRepository.Delete(user);
            return response;
        }
    }
}
