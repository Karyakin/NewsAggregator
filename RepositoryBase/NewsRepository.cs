using Microsoft.EntityFrameworkCore;
//using NewsAggregatorMain;
//using NewsAggregatorMain.Data;
using NewsAgregator.DAL.Entities.Entity.News;
using RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Migrations
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(NewsDataContext newsDataContext)
            : base(newsDataContext)
        {
        }

        /// <summary>
        /// trackChanges параметр. Мы собираемся использовать его для повышения производительности наших запросов только для чтения. 
        /// Если установлено значение false, мы присоединяемAsNoTracking в наш запрос, чтобы сообщить EF Core, 
        /// что ему не нужно отслеживать изменения для требуемых сущностей. Это значительно увеличивает скорость запроса.
        /// </summary>
        public async Task<News> FindNewsByIdAsync(Guid newsId, bool trackChanges)
        {
            var res1 = await FindAll(trackChanges).Where(news => news.Id.Equals(newsId)).FirstOrDefaultAsync();
            var res2 = await FindByCondition(news => news.Id.Equals(newsId), trackChanges).FirstOrDefaultAsync();
            return res2;
        }


    }
}
