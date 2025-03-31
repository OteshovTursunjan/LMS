using LMS.Application.DTO.Create;
using LMS.Application.Feature.TuitionFee.Queries;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.TuitionFee.Handler
{
    public  class GetTuiyionHandler : IRequestHandler<GetTuitionQueries, TuitionFeeModel>
    {
        private readonly ITuitionFeeRepository tuitionFeeRepository;
        public GetTuiyionHandler(ITuitionFeeRepository tuitionFeeRepository)
        {
            this.tuitionFeeRepository = tuitionFeeRepository;
        }

        public async Task<TuitionFeeModel> Handle(GetTuitionQueries request, CancellationToken cancellationToken)
        {
            var res = await tuitionFeeRepository.GetFirstAsync(u => u.id == request.id);

            if (res == null)
            {
                return null;
            }
            return new TuitionFeeModel
            {
                Summa = res.Summa,
                FacultyId = res.FacultyId,
            };
        }
    }
}
