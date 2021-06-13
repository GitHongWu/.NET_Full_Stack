using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ApplicationCore.Models.Response;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public AccountController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            //show a view with empty boxes for name, passward, email
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                //save to database
                var user = await _userService.RegisterUser(model);
                // redirect to Login
                return RedirectToAction("Login");
            }
            // take name, dob, email, pasword from view and save it to database
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated) return LocalRedirect("~/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel model)
        {
            var user = await _userService.Login(model.Email, model.Password);
            if (user == null)
            {
                return View();
            }

            // user entered his correct un/pw
            // we will create a cookie, movieshopauthcookie =>FirstName, LastName, id, Email, expiration time , claims
            // Cookie based Authentication.
            // 2 hours
            // 

            // create claims object and store required information
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, user.FirstName ),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            if (user.Roles != null) 
                claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // HttpContext => 
            // method type => get/post
            // Url
            // browsers
            // headers
            // form
            // cookies

            // create an identity object
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create a cookie that stores the identity information
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return LocalRedirect("~/");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index(int id = 0)
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            // check if user already logged in
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var profileModel = new UserProfileResponseModel
                {
                    Id = _currentUserService.UserId,
                    Email = _currentUserService.Email,
                    FullName = _currentUserService.FullName,
                };
                return View(profileModel);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }
    }
}
