using LMS.Application.DTO.Update;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Lesson.Command
{
    public record UpdateLessonCommand(LessonUpdateModel LessonUpdateModel) : IRequest<bool>;
    
    
}
