using Contracts.Interfaces;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
    public interface INewsService
    {
        public Task<IEnumerable<NewsGetDTO>> FindAllNews();
        Task<NewsGetDTO> GetNewsBiId(Guid? newsId);
        Task<IEnumerable<NewsInfoFromRssSourseDto>> GetNewsInfoFromRssSourse(RssSourceModel rssSourceDto);
        Task CreateOneNewsAsync(News news);
        Task CreateManyNewsAsync(IEnumerable<NewsInfoFromRssSourseDto> news);
        public Task RateNews();
        Task Aggregate();
        void Delete(News news);
        Task SaveAsync();
        void Save();
    }
}
