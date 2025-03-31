using LMS.Application.DTO.Create;
using LMS.Application.Feature.Contract.Queries;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Contract.Handler;

public class GetContractHandler : IRequestHandler<GetContractQueries, ContractCreateModel>
{
    public readonly IContractRepository contractRepository;
    public GetContractHandler(IContractRepository contractRepository)
    {
        this.contractRepository = contractRepository;
    }

    public async Task<ContractCreateModel> Handle(GetContractQueries request, CancellationToken cancellationToken)
    {
        var contract = await contractRepository.GetFirstAsync(u => u.id == request.id);
        if (contract == null)
            return null;
        return new ContractCreateModel
        {
            IsScholarship = contract.IsScholarship,
            UserID = contract.AccountID,
            TuitionFeeID = contract.TuitionFeeID,
        };
    }
}
