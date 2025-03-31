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
    public  class UpdateTuitionHandler : IRequestHandler<UpdateTuitionFeeCommand, bool>
    {
        private readonly ITuitionFeeRepository tuitionFeeRepository;
        public UpdateTuitionHandler(ITuitionFeeRepository tuitionFeeRepository)
        {
            this.tuitionFeeRepository = tuitionFeeRepository;
        }

        public async Task<bool> Handle(UpdateTuitionFeeCommand request, CancellationToken cancellationToken)
        {
            var tuition = await tuitionFeeRepository.GetFirstAsync(u => u.id == request.TuitionFeeUpdateModel.id);
            if (tuition == null) return false;

            tuition.FacultyId = request.TuitionFeeUpdateModel.FacultyId;
            tuition.Summa = request.TuitionFeeUpdateModel.Summa;
            await tuitionFeeRepository.UpdateAsync(tuition);

            return true;
        }
    }
}
