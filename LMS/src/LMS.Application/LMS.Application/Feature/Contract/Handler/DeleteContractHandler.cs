using LMS.Application.Feature.Contract.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Contract.Handler;

public  class DeleteContractHandler : IRequestHandler<DeleteContractCommand, bool>
{
    public readonly IContractRepository _contractRepository;
    public DeleteContractHandler(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }
    public async Task<bool> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetFirstAsync(u => u.id == request.id);
        if (contract == null)
            return false;
        await _contractRepository.DeleteAsync(contract);
        return true;
    }
}
