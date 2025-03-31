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
    public  class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, bool>
    {
        public readonly IGroupRepository _groupsRepository;
        public UpdateGroupHandler(IGroupRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }
        public async Task<bool> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupsRepository.GetFirstAsync(u => u.id == request.GroupUpdateModel.id);
            if (group == null) return false;
            group.Name = request.GroupUpdateModel.Name;
            group.Tutor = request.GroupUpdateModel.Tutor;


            await _groupsRepository.UpdateAsync(group);
            return true;
        }
    }
}
