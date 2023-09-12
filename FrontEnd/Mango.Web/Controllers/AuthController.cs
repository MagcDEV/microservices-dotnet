using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            ResponseDto responseDto = await _authService.LoginAsync(loginRequestDto);
            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = 
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                TempData["Success"] = "Login Successful";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", responseDto.Message);
                return View(loginRequestDto);
            }
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem{Text = SD.RoleCustomer, Value = SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            ResponseDto result = await _authService.RegisterAsync(registrationRequestDto);
            ResponseDto assingRole;

            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(registrationRequestDto.Role))
                {
                    registrationRequestDto.Role = SD.RoleCustomer;
                }

                assingRole = await _authService.AssingRolesAsync(registrationRequestDto);

                if (assingRole != null && assingRole.IsSuccess)
                {
                    TempData["Success"] = "Registration Successful";
                    return RedirectToAction("Login", "Auth");
                }

            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem{Text = SD.RoleCustomer, Value = SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;

            return View(registrationRequestDto);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            return View();
        }

    }
}