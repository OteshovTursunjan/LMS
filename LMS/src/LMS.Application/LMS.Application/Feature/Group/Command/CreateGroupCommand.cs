using LMS.Application.DTO.Create;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Group.Command
{
    public record CreateGroupCommand(GroupCreateModel GroupCreateModel) : IRequest<bool>;
    
    
}
