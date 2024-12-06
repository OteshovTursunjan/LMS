using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Context;
using LMS.Models;

namespace LMS.Repository
{
	public class SubjectRepository : Repository<Subject>, ISubjectRepository
	{
		private readonly AppDbContext _appDbContext;
		public SubjectRepository(AppDbContext appDbContext) : base(appDbContext)
		{
			_appDbContext = appDbContext;
		}

	}
}
