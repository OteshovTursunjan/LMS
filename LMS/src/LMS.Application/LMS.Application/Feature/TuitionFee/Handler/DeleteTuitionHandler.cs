using LMS.Application.Feature.TuitionFee.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.TuitionFee.Handler
{
    public  class DeleteTuitionHandler : IRequestHandler<DeleteTuitionFeeCommand , bool>
    {
        private readonly ITuitionFeeRepository tuitionFeeRepository;
        public DeleteTuitionHandler(ITuitionFeeRepository tuitionFeeRepository)
        {
            this.tuitionFeeRepository = tuitionFeeRepository;
        }

        public async Task<bool> Handle(DeleteTuitionFeeCommand request, CancellationToken cancellationToken)
        {
            var tuiton = await tuitionFeeRepository.GetFirstAsync(u => u.id == request.id);
            if (tuiton == null) return false;
            await tuitionFeeRepository.DeleteAsync(tuiton);
            return true;
        }
    }
}
