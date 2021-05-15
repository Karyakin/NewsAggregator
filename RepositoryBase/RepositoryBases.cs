using Microsoft.EntityFrameworkCore;
using Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.RepositoryInterfaces;
using Repositories.Context;
using System.Collections.Generic;
using Contracts.Interfaces;

namespace Repositories
{
    public abstract class RepositoryBases<T> : IRepositoryBases<T> where T : class, IBaseEntity
    {
        protected readonly NewsDataContext _Db;
        protected readonly DbSet<T> _Table;// тупо делаем переменную, чтобы каждый раз не писать DbSet<T>

        public RepositoryBases(NewsDataContext newsDataContext)
        {
            _Db = newsDataContext;
            _Table = _Db.Set<T>();//return table with type T ->GetSet(Type t). Если в IRepositoryBases<T> передадим News вернем таблицу от news и т.д
        }
        public async Task<T> GetById(Guid id, bool trackChanges)
        {
            var res = await _Table.FirstOrDefaultAsync(news => news.Id.Equals(id));
            return res;
        }
        public IQueryable<T> GetAll(bool trackChanges) => !trackChanges ? _Table.AsNoTracking() : _Table;


        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate,
             params Expression<Func<T, object>>[] includes)
        {
            var result = _Table.Where(predicate);
            if (includes.Any())
            {
                result = includes
                    .Aggregate(result,//Aggregate = выполнили запрос => результат ставим .Aggregate и над полученным результатом еще обработка. Каждый раз берем результат и выполняем над ним какие-то операции
                        (current, include)
                            => current.Include(include));
            }

            return result;
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
                    !trackChanges
                ?
                    _Table.Where(expression).AsNoTracking()
                :
                _Table.Where(expression);

        public void Add(T entity)
        {
            _Table.Add(entity);
        }

        public void AddRange(IEnumerable<T> entity)
        {
            _Table.AddRange(entity);
        }


        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _Table.Update(entity);
        }


        public void Dispose()
        {
            _Db?.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
