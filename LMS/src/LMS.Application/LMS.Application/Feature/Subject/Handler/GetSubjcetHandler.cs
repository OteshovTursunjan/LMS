using LMS.Application.DTO.Create;
using LMS.Application.Feature.Subject.Queries;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Subject.Handler;

public  class GetSubjcetHandler : IRequestHandler<GetSubjectQuereis, SubjectCreateModel>

{
    public readonly ISubjectRepository _subjectRepository;
    public GetSubjcetHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<SubjectCreateModel> Handle(GetSubjectQuereis request, CancellationToken cancellationToken)
    {
        var subjcet = await _subjectRepository.GetFirstAsync(u => u.id == request.id);
        if (subjcet == null) return null;
        return new SubjectCreateModel { FacultyID = subjcet.FacultyID, Name = subjcet.Name };
    }
}
