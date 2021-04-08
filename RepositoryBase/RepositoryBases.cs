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
        protected readonly DbSet<T> _Table;

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
                    .Aggregate(result,//Aggregate = выполнили запрос => результат ставим .Aggregate и над полученным результатом еще обработка
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




        /* public async Task<IQueryable<T>> GetAll(bool trackChanges)
         {
             var allEntity = await _Table.FirstOrDefaultAsync();
             return allEntity;
         }*/


        public Task Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }






        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            _Db?.Dispose();
            GC.SuppressFinalize(this);
        }


    }













    /*protected NewsDataContext _newsDataContext;
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
    public void Create(T entity) => _newsDataContext.Set<T>().Add(entity);
    public void Update(T entity) => _newsDataContext.Set<T>().Update(entity);
    public void Delete(T entity) => _newsDataContext.Set<T>().Remove(entity);
    public void CreateMany(IEnumerable<T> entitis) => _newsDataContext.Set<T>().AddRange(entitis);

    public void Dispose()
    {
        _newsDataContext.Dispose();// прекращает работу интерфейсов, чтобы при отключении базы запросы не выполнялись
        GC.SuppressFinalize(this);// Уборка мусора
    }*/




}
/*фактически мы оставляем возможность делать асинхронные и синхронные мотоды. Если нам подходят штатные мы берем их из
 быйсрепозитория, если нужны асинхронные и специфические берем из НьюсРепозитория
и мы фактически на частных интерфейса оборачиваем в несинхронную реализацию*/
