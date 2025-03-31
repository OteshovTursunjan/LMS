using LMS.Application.DTO.Create;
using LMS.Application.Feature.Teacher.Queries;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Teacher.Handler;

public  class GetTeacherHandler : IRequestHandler<GetTeacherQueries, TeacherCreateModel>
{
    public readonly ITeacherRepository teacherRepository;
    public GetTeacherHandler(ITeacherRepository teacherRepository)
    {
        this.teacherRepository = teacherRepository;
    }

    public async Task<TeacherCreateModel> Handle(GetTeacherQueries request, CancellationToken cancellationToken)
    {
        var teacher = await teacherRepository.GetFirstAsync(u => u.id == request.id);
        if (teacher == null) return null;
        return new TeacherCreateModel { FacultyID = teacher.FacultyID, FirstName = teacher.FirstName, LastName = teacher.LastName};
    }
}
