using LMS.Application.Feature.AcademicGrade.Command;
using LMS.Domain.Entity;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.AcademicGrade.Handler;

public class CreateAcademicGradeHandler : IRequestHandler<CreateAcademicGradeCommand, bool>
{
    private readonly IAcademinGradeRepository _academinGradeRepository;
    public CreateAcademicGradeHandler(IAcademinGradeRepository academinGradeRepository)
    {
        _academinGradeRepository = academinGradeRepository;
    }

    public async Task<bool> Handle(CreateAcademicGradeCommand request, CancellationToken cancellationToken)
    {
   //      var grade = await _academinGradeRepository.GetFirstAsync(u => u.AccountID == request.AcademicGradeCreateModel.UserId 
     // && u.SubjectId == request.AcademicGradeCreateModel.SubjectId);
       // if (grade != null)
       // {
       //     return false;
       // }
        if(request.AcademicGradeCreateModel.CurrentGrade > 30
            || request.AcademicGradeCreateModel.MidTerm > 30 || request.AcademicGradeCreateModel.FinaleExam > 50
             )
        {
            return false;
        }
        var newgrade = new LMS.Domain.Entity.AcademicGrade()
        {
            SubjectId = request.AcademicGradeCreateModel.SubjectId,
            AccountID = request.AcademicGradeCreateModel.UserId,
            CurrentGrade = request.AcademicGradeCreateModel.CurrentGrade,
            FinaleExam = request.AcademicGradeCreateModel.FinaleExam,
            MidTerm = request.AcademicGradeCreateModel.MidTerm,
           
        };
        await _academinGradeRepository.AddAsync(newgrade);
        var grade2 = await _academinGradeRepository.GetFirstAsync(u => u.SubjectId == newgrade.SubjectId 
        && u.AccountID == request.AcademicGradeCreateModel.UserId);
        int final = grade2.CurrentGrade + grade2.MidTerm + grade2.FinaleExam;
        if(final < 60)
        {
            grade2.IsFail = true;
        }
        grade2.OverallGrades = final;

        await _academinGradeRepository.UpdateAsync(grade2);
       return true;
    }
}
