using LMS.Application.Feature.Teacher.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Teacher.Handler
{
    public  class UpdateTeacherHandler : IRequestHandler<UpdateTeacherCommand, bool>
    {
        public readonly ITeacherRepository _teacherRepository;
        public UpdateTeacherHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public async Task<bool> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetFirstAsync(u => u.id == request.TeacherUpdateModel.id);
            if (teacher == null) return false;


            teacher.FirstName = request.TeacherUpdateModel.FirstName;
            teacher.LastName = request.TeacherUpdateModel.LastName;
            teacher.FacultyID = request.TeacherUpdateModel.FacultyID;
            await _teacherRepository.UpdateAsync(teacher);
            return true;
        }
    }
}
