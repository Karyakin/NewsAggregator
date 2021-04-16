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

    /// <summary>
    /// Сой вариант зависимостей и наследований
    /// </summary>
    public interface INewsService //: IRepositoryBases<News>
    {
        public Task<IEnumerable<NewsGetDTO>> FindAllNews();
        Task CreateOneNewsAsync(News news);
        Task CreateManyNewsAsync(IEnumerable<NewsInfoFromRssSourseDto> news);
        Task<NewsGetDTO> GetNewsBiId(Guid? newsId);
        Task<IEnumerable<NewsInfoFromRssSourseDto>> GetNewsInfoFromRssSourse(RssSourceModel rssSourceDto);

        Task<NewsInfoFromRssSourseDto> Delete();
        Task SaveAsync();
        void Save();
    }
}
