using LMS.Application.DTO.Create;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Faculty.Command
{
    public record CreateFacultyCommand (FacultyCreateModel FacultyCreateModel) : IRequest<bool>;
    
    
}
