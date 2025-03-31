using LMS.Application.Feature.Subject.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Subject.Handler;

public  class CreateSubjcetHandler : IRequestHandler<CreateSubjectCommand ,bool>
{
    public readonly ISubjectRepository _subjectRepository;
    public CreateSubjcetHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<bool> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = new LMS.Domain.Entity.Subject()
        {
            Name = request.SubjectCreateModel.Name,
            FacultyID = request.SubjectCreateModel.FacultyID,
        };
        await _subjectRepository.AddAsync(subject);
        return true;
    }
}
