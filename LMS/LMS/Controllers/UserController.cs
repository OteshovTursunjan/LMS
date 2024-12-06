using Microsoft.AspNetCore.Mvc;
using LMS.DTOs;
using LMS.Service;

namespace LMS.Controllers
{
    public class UserController : Controller
    {
        IAppService _appService;
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public UserController(IAppService appService)
        {
           _appService = appService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await _appService.GetToken(loginDTO);
            if(response?.Data  != null)
            {
                HttpContext.Response.Cookies.Append("AuthToken", response.Data.token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
                });
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = "Неправильный логин или пароль.";
            return View();
        }
    }
}
