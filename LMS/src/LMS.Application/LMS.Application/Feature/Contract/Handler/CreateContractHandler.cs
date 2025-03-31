using LMS.Application.Feature.Contract.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Contract.Handler;

public  class CreateContractHandler : IRequestHandler<CreateContractCommand, bool>
{
    public readonly IContractRepository _contractRepository;
    public CreateContractHandler(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<bool> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        var contract = new LMS.Domain.Entity.Contract()
        {
            AccountID = request.ContractCreateModel .UserID,
            IsScholarship = request.ContractCreateModel.IsScholarship,
            TuitionFeeID = request.ContractCreateModel.TuitionFeeID,

        };
        await _contractRepository.AddAsync(contract);
        return true;
    }
}
