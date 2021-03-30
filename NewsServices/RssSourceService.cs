using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.WrapperInterface;
using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RssSourceService : IRssSourceService
    {

        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;

        public RssSourceService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        public async Task CreateManyRssSource(IEnumerable<RssSource> rssSource)
        {
            _wrapper.RssSource.CreateManyRssSource(rssSource);
            await _wrapper.SaveAsync();
        }

        public async Task CreateOneRssSource(RssSource rssSource)
        {
            _wrapper.RssSource.CreateOneRssSource(rssSource);
            await _wrapper.SaveAsync();
        }

        public async Task<IEnumerable<RssSource>> GetAllRssSourceAsync(bool trackChanges) =>
           await _wrapper.RssSource.GetAllRssSourceAsync(false);

        public async Task<RssSource> RssSourceById(Guid rssSourceId) =>
            await _wrapper.RssSource.FindRssSourceById(rssSourceId);

        public async Task<RssSource> RssSourceByName(string rssSourceName) =>
            await _wrapper.RssSource.FindRssSourceByName(rssSourceName);
    }
}
