using LMS.Domain.Entity;
using LMS.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repository.lmpl
{
    public  class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(DatabaseContext databaseContext) : base(databaseContext) { }
    }
}
