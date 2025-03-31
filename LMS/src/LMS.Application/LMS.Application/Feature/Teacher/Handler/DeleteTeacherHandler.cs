using LMS.Application.Feature.Teacher.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Teacher.Handler;

public  class DeleteTeacherHandler  : IRequestHandler<DeleteTeacherCommand ,bool>
{
    public readonly ITeacherRepository teacherRepository;
    public DeleteTeacherHandler(ITeacherRepository teacherRepository)
    {
        this.teacherRepository = teacherRepository;
    }
    public async Task<bool> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await teacherRepository.GetFirstAsync(u => u.id == request.id);
        if (teacher == null) return false;
        await teacherRepository.DeleteAsync(teacher);
        return true;
    }
}
