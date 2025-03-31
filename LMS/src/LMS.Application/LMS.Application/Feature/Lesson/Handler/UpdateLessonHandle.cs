using LMS.Application.Feature.Lesson.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Lesson.Handler;

public  class UpdateLessonHandle : IRequestHandler<UpdateLessonCommand, bool>
{
    private readonly ILessonRepository lessonRepository;
    public UpdateLessonHandle(ILessonRepository lessonRepository)
    {
        this.lessonRepository = lessonRepository;
    }

    public async Task<bool> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
       var res = await lessonRepository.GetFirstAsync(u => u.id == request.LessonUpdateModel.id);
        if (res == null)
        {
            return false;
        }
        res.TeacherID = request.LessonUpdateModel.TeacherID;
        res.SubjcetID = request.LessonUpdateModel.SubjcetID;
        res.LessonTime = request.LessonUpdateModel.LessonTime;
        res.GroupID = request.LessonUpdateModel.GroupID;
        res.Room = request.LessonUpdateModel.Room;
        return true;
    }
}
