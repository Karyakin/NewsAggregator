using AutoMapper;
using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.WrapperInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Microsoft.EntityFrameworkCore;
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
            await _wrapper.News.Add(news);
            await _wrapper.SaveAsync();
        }


        public async Task<IEnumerable<NewsGetDTO>> FindAllNews()
        {
            var companies = await _wrapper.News.GetAll(false).ToListAsync();
            var getCompanyDTO = _mapper.Map<IEnumerable<NewsGetDTO>>(companies).ToList();
            return getCompanyDTO;
        }

       /* public async Task FindNewsBiId(Guid? newsId)
        {
            if (newsId.HasValue)
            {
                await _wrapper.News.FindAll(false).Where(x => x.Id.Equals(newsId)).FirstOrDefaultAsync();
            }
            else
            {
                await _wrapper.News.FindAll(false).ToListAsync();
            }
        }*/

        public async Task<NewsGetDTO> GetNewsBiId(Guid? newsId)
        {

           var news = await _wrapper.News.GetById(newsId.Value, false);

            return _mapper.Map<NewsGetDTO>(news);

        }

        public void Save() => _wrapper.Save();
        public Task SaveAsync() => _wrapper.SaveAsync();
    }
}
