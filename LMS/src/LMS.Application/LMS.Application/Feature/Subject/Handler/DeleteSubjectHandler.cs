using LMS.Application.Feature.Subject.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Subject.Handler;

public  class DeleteSubjectHandler : IRequestHandler<DeleteSubjectCommand, bool>
{
    public readonly ISubjectRepository _subjectRepository;
    public DeleteSubjectHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<bool> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var subjcet = await _subjectRepository.GetFirstAsync(u => u.id == request.id);
        if (subjcet == null)
            return false;
        await _subjectRepository.DeleteAsync(subjcet);
        return true;
    }
}
