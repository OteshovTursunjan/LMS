using LMS.Application.Feature.TuitionFee.Command;
using LMS.Domain.Entity;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.TuitionFee.Handler
{
    public  class CreationTuitionFeeHandler : IRequestHandler<CreateTuitioFeeCommand, bool>
    {
        private readonly ITuitionFeeRepository tuitionFeeRepository;
        public CreationTuitionFeeHandler(ITuitionFeeRepository tuitionFeeRepository)
        {
            this.tuitionFeeRepository = tuitionFeeRepository;
        }

        public async Task<bool> Handle(CreateTuitioFeeCommand request, CancellationToken cancellationToken)
        {
            var tuition = new LMS.Domain.Entity.TuitionFee()
            {
                Summa = request.TuitionFeeModel.Summa,
                FacultyId = request.TuitionFeeModel.FacultyId,

            };
            await tuitionFeeRepository.AddAsync(tuition);
            return true;
        }
    }
}
