using Entities.Entity.NewsEnt;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryInterfaces
{
    public interface INewsRepository : IRepositoryBase<News>
    {
        /*В Create а также Delete сигнатуры методов остаются синхронными.
        Это потому, что в этих методах мы не вносим никаких изменений в базу данных.
        Все, что мы делаем, это меняем состояние объекта на «Добавлено и удалено».*/


        Task<IEnumerable<News>> GetAllNewsAsync(bool trackChanges);
         void CreateOneNewsAsync(News news);
        Task<News> GetByIdsAsync(Guid ids, bool trackChanges);

    }
}

