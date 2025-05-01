using LMS.Domain.Entity;
using LMS.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repository.lmpl;

public  class TeacherRepository : BaseRepository<LMS.Domain.Entity.Teacher> , ITeacherRepository
{
    public TeacherRepository(DatabaseContext databaseContext) : base(databaseContext) { }
}
