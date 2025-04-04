using LMS.Application.Feature.Lesson.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Lesson.Handler;

public class CreateLessonHandler : IRequestHandler<CreateLessonCommand, bool>
{
    private readonly ILessonRepository _lessonsRepository;
    private readonly ISubjectRepository _subjectRepository;

    public CreateLessonHandler(ILessonRepository lessonsRepository, ISubjectRepository subjectRepository)
    {
        _lessonsRepository = lessonsRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<bool> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var lessonId = await _subjectRepository.GetFirstAsync(u => u.id == request.lessonCreateModel.SubjcetID);
        var lesson = new LMS.Domain.Entity.Lesson()
        {
            
            SubjcetID = lessonId.id,
            GroupID = request.lessonCreateModel.GroupID,
            TeacherID = request.lessonCreateModel.TeacherID,
            LessonTime = request.lessonCreateModel.LessonTime,
            
            Room = request.lessonCreateModel.Room,
        };
        await _lessonsRepository.AddAsync(lesson);
        return true;
    }
}