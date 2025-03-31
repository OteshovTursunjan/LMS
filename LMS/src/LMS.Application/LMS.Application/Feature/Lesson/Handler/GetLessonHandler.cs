
using LMS.Application.DTO.Create;
using LMS.Application.Feature.Lesson.Queries;
using LMS.Infrastructure.Repository;
using MediatR;

namespace LMS.Application.Feature.Lesson.Handler;

public class GetLessonHandler : IRequestHandler<GetLessonQueries, LessonCreateModel>
{
    private readonly ILessonRepository lessonRepository;
    public GetLessonHandler(ILessonRepository lessonRepository)
    {
        this.lessonRepository = lessonRepository;
    }

    public async Task<LessonCreateModel> Handle(GetLessonQueries request, CancellationToken cancellationToken)
    {
        var res = await lessonRepository.GetFirstAsync(u => u.id == request.id);
        if (res == null)
        {
            return null;
        }
        return new LessonCreateModel
        {
            LessonTime = res.LessonTime,
            TeacherID = res.TeacherID,
            Room = res.Room,
            SubjcetID = res.SubjcetID,
            GroupID = res.GroupID,
        };
    }
}
