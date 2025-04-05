using LMS.Application.Feature.User.Command;
using LMS.Domain.Entity;
using LMS.Domain.Identity;
using LMS.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.User.Handler;

public class RegisterTeacherHandler : IRequestHandler<RegisterTeacherCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ITeacherRepository _teacherRepository;

    public RegisterTeacherHandler(UserManager<ApplicationUser> userManager, ITeacherRepository teacherRepository,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
        _teacherRepository = teacherRepository;
    }
    public async Task<bool> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
    {

        var userexist = await _userManager.FindByEmailAsync(request.teacherCreateModel.Email);

        // return BadRequest(new { message = "Foydalanuvchi allaqachon mavjud!" });
        var teacher = new Teacher()
        {
            FirstName = request.teacherCreateModel.FirstName,
            LastName = request.teacherCreateModel.LastName,
            FacultyID = request.teacherCreateModel.FacultyID,
        };
        await _teacherRepository.AddAsync(teacher);
        var user = new ApplicationUser()
        {
            UserName = request.teacherCreateModel.FirstName,
            LastName = request.teacherCreateModel.LastName,
            Email = request.teacherCreateModel.Email,
            
            FirstName = request.teacherCreateModel.FirstName
        };
        var result = await _userManager.CreateAsync(user, request.teacherCreateModel.Password);
        return true;
    }
}
