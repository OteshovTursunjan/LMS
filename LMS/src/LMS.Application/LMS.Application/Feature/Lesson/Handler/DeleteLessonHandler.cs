using LMS.Application.Feature.Lesson.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Lesson.Handler;

public  class DeleteLessonHandler : IRequestHandler<DeleteLessonCommand, bool>
{
    public readonly ILessonRepository _lessonsRepository;
    public DeleteLessonHandler(ILessonRepository lessonsRepository)
    {
        _lessonsRepository = lessonsRepository;
    }

    public async Task<bool> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonsRepository.GetFirstAsync(u => u.id == request.id);
        if (lesson == null) return false;

        await _lessonsRepository.DeleteAsync(lesson);
        return true;
    }
}
