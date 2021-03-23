using Microsoft.EntityFrameworkCore;
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


        public void DeleteNews(News news) => Delete(news);
        public void UpdateNews(News news) => Update(news);


        public async Task CreateAsync(News news)
        {
            await CreateAsync(news);
        }
        
        public async Task<IEnumerable<News>> GetAllNewsAsync(bool trackChanges)
        {
            var res = await FindAll(trackChanges).ToListAsync();
            var res1 = res;
            return res1;
        }
        public async Task<IEnumerable<News>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            var res = await FindByCondition(news => news.Id.Equals(ids), trackChanges).ToListAsync();
            return res;
        }
        public async Task<News> GetNewsAsync(Guid newsId, bool trackChanges)
        {
            var res = await FindByCondition(x => x.Id.Equals(newsId), trackChanges).SingleOrDefaultAsync();
            return res;
        }
    }
}
