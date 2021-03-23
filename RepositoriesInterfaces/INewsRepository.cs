using NewsAgregator.DAL.Entities.Entity.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesInterfaces
{
    public interface INewsRepository : IRepositoryBase<News>
    {
        /*В Create а также Delete сигнатуры методов остаются синхронными.
        Это потому, что в этих методах мы не вносим никаких изменений в базу данных.
        Все, что мы делаем, это меняем состояние объекта на «Добавлено и удалено».*/
        Task<News> FindNewsByIdAsync(Guid newsId, bool trackChanges);


    }
}
