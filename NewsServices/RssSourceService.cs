﻿using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Entities.Models.AssembledModel;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Services
{
    public class RssSourceService : IRssSourceService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RssSourceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateManyRssSource(IEnumerable<RssSource> rssSource)
        {
            _unitOfWork.RssSource.AddRange(rssSource);
            await _unitOfWork.SaveAsync();
        }

        public async Task CreateOneRssSource(RssSourceModel rssSourceModel)
        {

            if (rssSourceModel is null)
            {
                throw new ArgumentNullException("Incorrect date for creation RssSourse");
            }

            try
            {
                var rssSourceEntity = _mapper.Map<RssSource>(rssSourceModel);

                _unitOfWork.RssSource.Add(rssSourceEntity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong when trying to add to the source. Message: {ex.Message}");
                throw new ArgumentException("Incorrect Rss Sourse adress!");
            }
        }

        public async Task<IEnumerable<RssSourceModel>> GetAllRssSourceAsync(bool trackChanges)
        {
            var rssSourse = await _unitOfWork.RssSource.GetAll(false).ToListAsync();
            var rssSourseDto = _mapper.Map<IEnumerable<RssSourceModel>>(rssSourse).ToList();
            return rssSourseDto;

        }

        public async Task<RssSourceModel> RssSourceById(Guid? rssSourceId)
        {
            var rssmodel = await _unitOfWork.RssSource.GetById(rssSourceId.Value, false);
            var res = _mapper.Map<RssSourceModel>(rssmodel);
            return res;
        }

        public async Task<SourseWithNewsCategory> RssSourceByIdWithNews(Guid? rssSourceId)
        {

            // var resoult =  _unitOfWork.RssSource.GetBy(n => n.Id.Equals(rssSourceId.Value), n => n.News);


            var rssSourseWithNews = await _unitOfWork.RssSource.GetByCondition(x => x.Id.Equals(rssSourceId), true)
               .Include(news => news.News)
               .ThenInclude(z => z.Category)
               .Include(x => x.News)//-- отсюда можно не делать, это тут не нужно и чисто для примера инклудов
               .ThenInclude(x => x.Comments).SingleOrDefaultAsync();


            var rssSourceWithNews = _mapper.Map<SourseWithNewsCategory>(rssSourseWithNews);

            return rssSourceWithNews;
        }

        public async Task<RssSource> RssSourceByName(string rssSourceName)
        {
            //   await _wrapper.RssSource.FindRssSourceByName(rssSourceName);
            return null;
        }
    }
}
