using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryInterfaces
{
    public interface IRepositoryBase<T>
    {
        /*В Create а также Delete сигнатуры методов остаются синхронными.
        Это потому, что в этих методах мы не вносим никаких изменений в базу данных.
        Все, что мы делаем, это меняем состояние объекта на «Добавлено и удалено».*/
       
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void CreateMany(IEnumerable<T> entitis);


        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    }
}
