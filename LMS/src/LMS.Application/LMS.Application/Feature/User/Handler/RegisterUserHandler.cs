using LMS.Application.Feature.User.Command;
using LMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.User.Handler;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public RegisterUserHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userexist = await _userManager.FindByEmailAsync(request.model.Email);
       
        // return BadRequest(new { message = "Foydalanuvchi allaqachon mavjud!" });

        var user = new ApplicationUser()
        {
            UserName = request.model.FirstName,
            LastName = request.model.LastName,
            Email = request.model.Email,
            FirstName = request.model.FirstName
        };
        var result = await _userManager.CreateAsync(user, request.model.Password);
        return true;
    }
}
