using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
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
        Task CreateOneNewsAsync(News news);
        Task SaveAsync();
        void Save();
    }
}
