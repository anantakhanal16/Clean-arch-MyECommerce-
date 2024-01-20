using Core.Entites.Identity;
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mye_CommerceApp.Dtos;
using System.Security.Claims;

namespace Mye_CommerceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService;
    
        public AccountController( IAuthenticationService authService)
        {
            _authService = authService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("~/Views/Account/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Status>> LoginComplete(LoginDto loginDto)
        {
            UserLogin model = new UserLogin();

            model.UserName = loginDto.Email;
            model.Password = loginDto.Password;


            var status = await _authService.LoginAsync(model);
           
            if (status.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("~/Views/Account/Index.cshtml", model);
            }
        }

        public async Task<ActionResult> LogOut()
        {
            await _authService.LogoutAsync();

            return RedirectToAction("Login");
        }


        public async Task<ActionResult<UserDto>> Signup()
        {
            return View("~/Views/Account/Signup.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Status>> SignupComplete(RegisterDto registerDto)
        {
            RegistrationModel Data = new RegistrationModel();
           
            Data.Email = registerDto.Email;
            Data.Password = registerDto.Password;
            Data.DisplayName = registerDto.DisplayName;
            Data.Name = registerDto.DisplayName;
            Data.Role = "User";

            var status  = await _authService.RegistrationAsync(Data);

            if (status.StatusCode == 1) 
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View("~/Views/Account/Signup.cshtml", registerDto);
            }
        }

        //public async Task<ActionResult<Status>> SignupComplete()
        //{
        //    RegistrationModel Data = new RegistrationModel();

        //    Data.Email = "Admin123@gmail.com";
        //    Data.Password = "Admin@12343#";
        //    Data.DisplayName = "Admin1";
        //    Data.Name = "Admin";
        //    Data.Role = "Admin";

        //    var status = await _authService.RegistrationAsync(Data);

        //    return status;
        //}
    }
}
