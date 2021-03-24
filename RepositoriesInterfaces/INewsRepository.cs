using Entities.Entity.News;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface INewsRepository : IRepositoryBase<News>
    {
        /*В Create а также Delete сигнатуры методов остаются синхронными.
        Это потому, что в этих методах мы не вносим никаких изменений в базу данных.
        Все, что мы делаем, это меняем состояние объекта на «Добавлено и удалено».*/
      

       /* Task<IEnumerable<News>> GetAllNewsAsync(bool trackChanges);
        Task<News> GetNewsAsync(Guid newsId, bool trackChanges);
        Task<IEnumerable<News>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

        Task CreateAsync(News news);
        
        // я думаю эти два метода нужно коментировать и они должнв работать из бэйсрепозиторий
        void DeleteNews(News news);// обязательно проверить если закомитать этот метод подтянется ли все из repositoryBase
        void UpdateNews(News news);*/
    }
}

