

using LMS.Application.Feature.Subject.Command;
using LMS.Infrastructure.Repository;
using MediatR;

namespace LMS.Application.Feature.Subject.Handler;

public  class UpdateSubjectHandler : IRequestHandler<UpdateSubjectCommand, bool>
{
    public readonly ISubjectRepository _subjectRepository;
    public UpdateSubjectHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<bool> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subjcet = await _subjectRepository.GetFirstAsync(u => u.id == request.SubjectUpdateModel.id);
        if (subjcet == null) return false;

        subjcet.Name = request.SubjectUpdateModel.Name;
        subjcet.FacultyID = request.SubjectUpdateModel.FacultyID;
        await _subjectRepository.UpdateAsync(subjcet);
        return true;
    }
}
