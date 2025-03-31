using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Faculty.Command
{
    public record DeleteFacultyCommand(Guid id) : IRequest<bool>;
    
    
}
