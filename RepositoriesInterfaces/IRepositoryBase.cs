using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesInterfaces
{
    public interface IRepositoryBase<T>
    {
        Task GetById(T entity);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<IQueryable<T>> FindAll(bool trackChanges);
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    }
}
