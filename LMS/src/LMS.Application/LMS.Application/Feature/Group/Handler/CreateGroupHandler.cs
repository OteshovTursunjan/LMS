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
    public  class CreateGroupHandler : IRequestHandler<CreateGroupCommand , bool>
    {
        public readonly IGroupRepository _groupsRepository;
        public CreateGroupHandler(IGroupRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }

        public async Task<bool> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new LMS.Domain.Entity.Group()
            {
                Name = request.GroupCreateModel .Name,
                Tutor = request.GroupCreateModel.Tutor,
            };
            await _groupsRepository.AddAsync(group);
            return true;
        }
    }
}
