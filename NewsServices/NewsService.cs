using AutoMapper;
using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.WrapperInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class NewsService : INewsService
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;



        public NewsService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        public async Task CreateOneNewsAsync(News news)
        {
            _wrapper.News.Create(news);
            await _wrapper.SaveAsync();
        }

        public async Task<IEnumerable<NewsGetDTO>> FindAllNews()
        {
            var companies = await _wrapper.News.GetAllNewsAsync(false);
            var getCompanyDTO = _mapper.Map<IEnumerable<NewsGetDTO>>(companies).ToList();
            return getCompanyDTO;
        }

        public void Save() => _wrapper.Save();
        public Task SaveAsync() => _wrapper.SaveAsync();
    }
}
