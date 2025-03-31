using LMS.Application.Feature.Faculty.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Faculty.Handler
{
    public  class UpdateFacultyHandler : IRequestHandler<UpdateFacultyCommand, bool>
    {
        public readonly IFacultyRepository _facultyRepository;
        public UpdateFacultyHandler(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<bool> Handle(UpdateFacultyCommand request, CancellationToken cancellationToken)
        {
            var faculty = await _facultyRepository.GetFirstAsync(u => u.id == request.FacultyUpdateModel.id);
            if (faculty == null) return false;

            var newFaculty = new LMS.Domain.Entity.Faculty()
            {
                Name = request.FacultyUpdateModel.Name,
                Dekan = request.FacultyUpdateModel.Dekan,
            };
            await _facultyRepository.UpdateAsync(newFaculty);
            return true;
        }
    }
}
