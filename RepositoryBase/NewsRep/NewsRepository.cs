using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.RepositoryInterfaces;
using Repositories.Context;
using Entities.Entity.NewsEnt;

namespace Repositories.NewsRep
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(NewsDataContext newsDataContext)
            : base(newsDataContext)
        {
        }
      //  https://www.youtube.com/watch?v=S4YDarQBkiM&ab_channel=CodeMaze

        /// <summary>
        /// trackChanges параметр. Мы собираемся использовать его для повышения производительности наших запросов только для чтения. 
        /// Если установлено значение false, мы присоединяемAsNoTracking в наш запрос, чтобы сообщить EF Core, 
        /// что ему не нужно отслеживать изменения для требуемых сущностей. Это значительно увеличивает скорость запроса.
        /// </summary>


        public void UpdateNews(News news) => Update(news);
        public void DeleteNews(News news) =>  Delete(news);

        public async Task<IEnumerable<News>> GetAllNewsAsync(bool trackChanges)
        {
            var res = await FindAll(trackChanges).ToListAsync();
            var res1 = res;
            return res1;
        }
        public async Task<News> GetByIdsAsync(Guid ids, bool trackChanges)
        {
            var res = await FindByCondition(news => news.Id.Equals(ids), trackChanges).FirstOrDefaultAsync();
            return res;
        }
        public async Task<IEnumerable<News>> GetNewsAsync(IEnumerable<Guid> newsId, bool trackChanges)
        {
            var res = await FindByCondition(x => x.Id.Equals(newsId), trackChanges).ToListAsync();
            return res;
        }

        public void CreateOneNewsAsync(News news)=>Create(news);

    }
}
