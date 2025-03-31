using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Contract.Command
{
    public record DeleteContractCommand(Guid id) : IRequest<bool>;
    
    
}
