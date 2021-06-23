using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Models.Request;

namespace BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /api​/Users
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        // GET /api​/Users/{id}
        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null) return NotFound("User Id does not exist");
            return Ok(user);
        }

        // GET /api​/Users​/Details/{id}
        [HttpGet]
        [Route("Details/{id:int}", Name = "GetUserDetails")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var userDetails = await _userService.GetUserDetailsById(id);
            if (userDetails != null)
            {
                return Ok(userDetails);
            }
            return NotFound("no user found");
        }

        // POST /api​/Users​/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> AddUser([FromBody] RegisterUserRequestModel model)
        {
            if (await _userService.UserExistByEmail(model.Email))
                return BadRequest("Email already exist, try login");

            var response = await _userService.RegisterUser(model);
            return Ok(response);
        }

        // PUT /api​/Users​/Update
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequestModel model)
        {
            var user = await _userService.UpdateUser(model);
            if (user == null) return NotFound("User email does not exist");
            return Ok(user);
        }

        // DELETE /api​/Users​/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.DeleteUser(id);
            if (user == null) return NotFound("User Id does not exist");
            return Ok("Delete Successed!");
        }
    }
}
