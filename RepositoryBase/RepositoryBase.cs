using Microsoft.EntityFrameworkCore;
using Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.RepositoryInterfaces;
using Repositories.Context;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected NewsDataContext _newsDataContext;
        public RepositoryBase(NewsDataContext newsDataContext)
        {
            _newsDataContext = newsDataContext;
        }



        public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges 
            ?
              _newsDataContext.Set<T>().AsNoTracking()
            : 
                _newsDataContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges
            ?
             _newsDataContext.Set<T>().Where(expression).AsNoTracking()
            :
            _newsDataContext.Set<T>().Where(expression);

        public void Create(T entity) => _newsDataContext.Set<T>().AddRangeAsync(entity);
        public void Update(T entity) => _newsDataContext.Set<T>().Update(entity); 
        public void Delete(T entity) => _newsDataContext.Set<T>().Remove(entity);
    }

}
/*фактически мы оставляем возможность делать асинхронные и синхронные мотоды. Если нам подходят штатные мы берем их из
 быйсрепозитория, если нужны асинхронные и специфические берем из НьюсРепозитория
и мы фактически на частных интерфейса оборачиваем в несинхронную реализацию*/
