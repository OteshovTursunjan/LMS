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
    public  class CreateFacultyHandler : IRequestHandler<CreateFacultyCommand,bool>
    {
        public readonly IFacultyRepository _facultyRepository;
        public CreateFacultyHandler(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }
        public async Task<bool> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
        {
            var faculty = new LMS.Domain.Entity.Faculty()
            {
                Dekan = request.FacultyCreateModel.Dekan,
                Name = request.FacultyCreateModel.Name,
            };
            await _facultyRepository.AddAsync(faculty);
            return true;
        }
    }
}
