using AutoMapper;
using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsService(IUnitOfWork wrapper, IMapper mapper)
        {
            _unitOfWork = wrapper;
            _mapper = mapper;
        }


        public async Task CreateOneNewsAsync(News news)
        {
            await _unitOfWork.News.Add(news);
            await _unitOfWork.SaveAsync();
        }


        public async Task<IEnumerable<NewsGetDTO>> FindAllNews()
        {
            var companies = await _unitOfWork.News.GetAll(false).ToListAsync();

            if (companies is null)
            {
                Log.Error($"Colled method{nameof(FindAllNews)} returned null.");
                throw new ArgumentNullException("while trying to get news, something went wrong. No values ​​received.");
            }
            var getCompanyDTO = _mapper.Map<IEnumerable<NewsGetDTO>>(companies).ToList();

            
            return getCompanyDTO;
        }


        public async Task<NewsGetDTO> GetNewsBiId(Guid? newsId)
        {

           var news = await _unitOfWork.News.GetById(newsId.Value, false);

            return _mapper.Map<NewsGetDTO>(news);

        }

        public void Save() => _unitOfWork.Save();
        public Task SaveAsync() => _unitOfWork.SaveAsync();
    }
}
