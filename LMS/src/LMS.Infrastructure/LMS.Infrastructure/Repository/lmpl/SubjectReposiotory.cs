using LMS.Domain.Entity;
using LMS.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repository.lmpl
{
    public  class SubjectReposiotory : BaseRepository<Subject> ,ISubjectRepository
    {
        public SubjectReposiotory(DatabaseContext databaseContext) : base(databaseContext) { }
    }
}
