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
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Text;
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

        public async Task RateNews()
        {
            var newsText = "В связи с резким ростом числа случаев заболевания COVID-19 мэрия" +
                " Москва продлила нерабочие дни с 15 по 19 июня включительно с сохранением заработной платы. " +
                "Суточный показатель заражений по всей России за последнюю неделю вырос почти вдвое, до более чем 13,5 тыс человек. " +
                "Предприятиям рекомендовано вернуть на удаленку как можно больше сотрудников, не прошедших вакцинацию.";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=8a31806eafe746700c6afb702dc087fb63d63e75")
                {
                    Content = new StringContent("[{\"text\":\"" + newsText + "\"}]",

                        Encoding.UTF8,
                        "application/json")
                };
                var response = await httpClient.SendAsync(request);

                var responseString = await response.Content.ReadAsStringAsync();
            }
        }


        public  Task Aggregate()
        {
            throw new NotImplementedException();
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
