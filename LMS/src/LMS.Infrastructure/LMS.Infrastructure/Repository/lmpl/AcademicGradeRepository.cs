using LMS.Domain.Entity;
using LMS.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repository.lmpl
{
    public class AcademicGradeRepository : BaseRepository<AcademicGrade>, IAcademinGradeRepository
    {
        public AcademicGradeRepository(DatabaseContext databaseContext) : base(databaseContext) { }
    }
}
