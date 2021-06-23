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
        Task<List<UserProfileResponseModel>> GetAllUsers();
        Task<UserProfileResponseModel> GetUserById(int userId);
        Task<List<UserDetailResponseModel>> GetUserDetailsById(int id);
        Task<bool> UserExistByEmail(string email);
        Task<UserProfileResponseModel> RegisterUser(RegisterUserRequestModel model);
        Task<UserProfileResponseModel> UpdateUser(UserUpdateRequestModel model);
        Task<UserProfileResponseModel> DeleteUser(int userId);
    }
}
