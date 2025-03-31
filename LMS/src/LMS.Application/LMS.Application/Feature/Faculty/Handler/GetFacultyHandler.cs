using LMS.Application.DTO.Create;
using LMS.Application.Feature.Faculty.Queries;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Faculty.Handler
{
    public  class GetFacultyHandler : IRequestHandler<GetFacultyQueries, FacultyCreateModel>
    {
        public readonly IFacultyRepository _facultyRepository;
        public GetFacultyHandler(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }
        public async Task<FacultyCreateModel> Handle(GetFacultyQueries request, CancellationToken cancellationToken)
        {
            var faculty = await _facultyRepository.GetFirstAsync(u => u.id == request.id);

            if (faculty == null) return null;

            return new FacultyCreateModel
            {
                Dekan = faculty.Dekan,
                Name = faculty.Name
            };
        }
    }
}
