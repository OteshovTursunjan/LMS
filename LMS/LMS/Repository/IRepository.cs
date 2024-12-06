using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<TEntity> CreateAsync(TEntity entity);
		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

		Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression);

		Task<TEntity> UpdateAsync(TEntity entity);
		Task<bool> DeleteAsync(TEntity entity);
	}
}
