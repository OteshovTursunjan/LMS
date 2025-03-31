using LMS.Application.Feature.Group.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Group.Handler
{
    public  class DeleteGroupHandler : IRequestHandler<DeleteGroupCommand, bool>
    {
        public readonly IGroupRepository _groupsRepository;
        public DeleteGroupHandler(IGroupRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }
        public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var groups = await _groupsRepository.GetFirstAsync(u => u.id == request.id);
            if (groups == null)
                return false;
            await _groupsRepository.DeleteAsync(groups);
            return true;
        }
    }
}
