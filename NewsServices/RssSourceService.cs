﻿using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Entities.Models.AssembledModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAgregato.DAL.CQRS.Queries;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class RssSourceService : IRssSourceService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RssSourceService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
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
            try
            {
                var rssSourse = await _unitOfWork.RssSource.GetAll(false).ToListAsync();
                var rssSourseDto = _mapper.Map<IEnumerable<RssSourceModel>>(rssSourse).ToList();
                return rssSourseDto;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<RssSourceModel> GetRssSourceById(Guid? rssSourceId)
        {
            try
            {
                var rssSourseQuery = new GetRssSourseByIdQuery(rssSourceId.Value);
                var rssSourseDto = await _mediator.Send(rssSourseQuery);
                return _mapper.Map<RssSourceModel>(rssSourseDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<RssSourceModel>> RssSourceByNameAndUrl(string name, string url)
        {
            try
            {
                var rssSourseQuery = new GetRssSourseByNameAndUrlQuery(name, url);
                var rssSourseDto = await _mediator.Send(rssSourseQuery);
                return _mapper.Map<IEnumerable<RssSourceModel>>(rssSourseDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }


        public async Task<SourseWithNewsCategory> RssSourceByIdWithNews(Guid? rssSourceId)
        {
            try
            {
               var rssSourseWithNews = await _unitOfWork.RssSource.GetByCondition(x => x.Id.Equals(rssSourceId), true)
              .Include(news => news.News)
              .ThenInclude(z => z.Category)
              .Include(x => x.News)//-- отсюда можно не делать, это тут не нужно и чисто для примера инклудов
              .ThenInclude(x => x.Comments).SingleOrDefaultAsync();

                return _mapper.Map<SourseWithNewsCategory>(rssSourseWithNews);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        public Task<int> DeleteRssSourse(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteNews(SourseWithNewsCategory sourseWithNewsCategory)
        {
            throw new NotImplementedException();
        }
    }
}
