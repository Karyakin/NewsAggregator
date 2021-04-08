using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IRepositoryBases<T> : IDisposable where T : class, IBaseEntity
    {
        Task<T> GetById(Guid id, bool trackChanges);
        IQueryable<T> GetAll(bool trackChanges);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task Add(T entity);
        Task AddRange(IEnumerable<T> entity);

        Task Update(T entity);
        Task Remove(Guid id);
        Task RemoveRange(IEnumerable<T> entity);
    }
}
