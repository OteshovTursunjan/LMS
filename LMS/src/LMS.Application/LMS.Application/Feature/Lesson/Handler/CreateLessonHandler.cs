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
    public readonly ILessonRepository _lessonsRepository;
    public CreateLessonHandler(ILessonRepository lessonsRepository)
    {
        _lessonsRepository = lessonsRepository;
    }

    public async Task<bool> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = new LMS.Domain.Entity.Lesson()
        {
            SubjcetID = request.lessonCreateModel.SubjcetID,
            GroupID = request.lessonCreateModel.GroupID,
            TeacherID = request.lessonCreateModel.TeacherID,
            LessonTime = request.lessonCreateModel.LessonTime,

            Room = request.lessonCreateModel.Room,
        };
        await _lessonsRepository.AddAsync(lesson);
        return true;
    }
}