using AngleSharp;
using AutoMapper;
using Contracts.ParseInterface;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAgregato.DAL.CQRS.Queries;
using NewsAgregato.DAL.CQRS.QueryHendlers;
using Serilog;
using Services.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace Services
{
    public class CQRSNewsService : INewsService
    {


        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly INewsService _newsService;

        public CQRSNewsService(IMapper mapper, IMediator mediator, INewsService newsService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _newsService = newsService;
        }

        public async Task Aggregate()
        {
            var query = new GetAllRssSourseQuery();
            var rssSourse = await _mediator.Send(query);


            Parallel.ForEach(rssSourse, (sourse)=> _newsService.GetNewsInfoFromRssSourse(sourse));


           /*  foreach (var item in rssSourse)
            {
               await _newsService.GetNewsInfoFromRssSourse(item);

            }*/



        }

        public Task CreateManyNewsAsync(IEnumerable<NewsInfoFromRssSourseDto> news)
        {
            throw new NotImplementedException();
        }

        public Task CreateOneNewsAsync(News news)
        {
            throw new NotImplementedException();
        }

        public Task<NewsInfoFromRssSourseDto> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsGetDTO>> FindAllNews()
        {
            throw new NotImplementedException();
        }

        public Task<NewsGetDTO> GetNewsBiId(Guid? newsId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsInfoFromRssSourseDto>> GetNewsInfoFromRssSourse(RssSourceModel rssSourceDto)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
    }
