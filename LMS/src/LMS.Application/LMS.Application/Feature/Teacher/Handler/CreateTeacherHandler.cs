using LMS.Application.Feature.Teacher.Command;
using LMS.Domain.Entity;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Teacher.Handler;

public class CreateTeacherHandler : IRequestHandler<CreateTeacherCommand, bool>
{
    public readonly ITeacherRepository teacherRepository;
    public CreateTeacherHandler(ITeacherRepository teacherRepository)
    {
        this.teacherRepository = teacherRepository;
    }
    public async Task<bool> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = new Domain.Entity.Teacher()
        {
            FirstName = request.teacherDTO.FirstName,
            LastName = request.teacherDTO.LastName,
            FacultyID = request.teacherDTO.FacultyID,
        };
        await teacherRepository.AddAsync(teacher);
        return true;
    }
}
