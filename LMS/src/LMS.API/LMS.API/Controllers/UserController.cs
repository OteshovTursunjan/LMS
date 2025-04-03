using LMS.Application.DTO.User;
using LMS.Application.Feature.User.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
  
    public class UserController : Controller
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPut("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserModel registerUserModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await mediator.Send(new RegisterUserCommand(registerUserModel));
            return Ok(res);
        }
        [HttpPut("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginUserModel loginUserModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await mediator.Send(new  LoginUserCommand(loginUserModel));
            return Ok(res);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
