using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.WrapperInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Entities.Models.AssembledModel;
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

        public async Task CreateOneRssSource(RssSourceModel rssSourceModel)
        {
            var rssSourceEntity= _mapper.Map<RssSource>(rssSourceModel);
            _wrapper.RssSource.CreateOneRssSource(rssSourceEntity);
            await _wrapper.SaveAsync();
        }

        public async Task<IEnumerable<RssSourceModel>> GetAllRssSourceAsync(bool trackChanges)
        {
            var rssSourse = await _wrapper.RssSource.GetAllRssSourceAsync(false);
            var rssSourseDto = _mapper.Map<IEnumerable<RssSourceModel>>(rssSourse).ToList();
            return rssSourseDto;

        }

        public async Task<RssSourceModel> RssSourceById(Guid? rssSourceId)
        {
            var rssmodel = await _wrapper.RssSource.FindRssSourceById(rssSourceId.Value);
            var res = _mapper.Map<RssSourceModel>(rssmodel);
            return res;
        }

        public async Task<NewForSourse> RssSourceByIdWithNews(Guid? rssSourceId)
        {
            var rssSource = await _wrapper.RssSource.FindNewsForSourse(rssSourceId.Value);
            var rssSourceWithNews = _mapper.Map<NewForSourse>(rssSource);

            return rssSourceWithNews;
        }

        public async Task<RssSource> RssSourceByName(string rssSourceName) =>
            await _wrapper.RssSource.FindRssSourceByName(rssSourceName);
    }
}
