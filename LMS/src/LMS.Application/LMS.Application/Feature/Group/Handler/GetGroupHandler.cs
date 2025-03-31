using LMS.Application.DTO.Create;
using LMS.Application.Feature.Group.Queries;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Group.Handler
{
    public class GetGroupHandler : IRequestHandler<GetGroupQueries, GroupCreateModel>
    {
        private readonly IGroupRepository groupRepository;
        public GetGroupHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }
        public async Task<GroupCreateModel> Handle(GetGroupQueries request, CancellationToken cancellationToken)
        {
           var group = await groupRepository.GetFirstAsync(u => u.id == request.id);
            if (group == null)
            {
                return null;
            }
            return new GroupCreateModel
            {
                Name = group.Name,
                Tutor = group.Tutor,
            };
        }
    }
}
