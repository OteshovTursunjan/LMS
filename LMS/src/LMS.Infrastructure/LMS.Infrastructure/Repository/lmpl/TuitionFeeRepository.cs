
using LMS.Domain.Entity;
using LMS.Infrastructure.Persistance;

namespace LMS.Infrastructure.Repository.lmpl;

public class TuitionFeeRepository : BaseRepository<TuitionFee>, ITuitionFeeRepository
{
    public TuitionFeeRepository(DatabaseContext databaseContext) : base(databaseContext) { }
}
