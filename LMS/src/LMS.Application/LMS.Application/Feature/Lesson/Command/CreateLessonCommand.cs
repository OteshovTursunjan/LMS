using LMS.Application.DTO.Create;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Lesson.Command
{
    public  record CreateLessonCommand(LessonCreateModel lessonCreateModel) : IRequest<bool>;
    
}
