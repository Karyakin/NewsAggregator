using AutoMapper;
using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

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

        public async Task CreateManyNewsAsync(IEnumerable<NewsInfoFromRssSourseDto> newsInfoFromRssSourseDtos)
        {
            try
            {
                //var news =  _mapper.Map<IEnumerable<News>>(newsInfoFromRssSourseDtos);

                var test = newsInfoFromRssSourseDtos;

                var news = _mapper.Map<IEnumerable<News>>(newsInfoFromRssSourseDtos);


                _unitOfWork.News.AddRange(news);
                await _unitOfWork.SaveAsync();

            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }



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

        public async Task<IEnumerable<NewsInfoFromRssSourseDto>> GetNewsInfoFromRssSourse(RssSourceModel rssSourceModel)
        {
            var news = new List<NewsInfoFromRssSourseDto>();
            var categories = new List<Category>();

            using (var reader = XmlReader.Create(rssSourceModel.Link))
            {
                var feed = SyndicationFeed.Load(reader);
                reader.Close();




                if (feed.Items.Any())
                {
                    var rssSoursesId = await _unitOfWork.Category.GetAll(false).Select(z => z.Name).ToListAsync();

                    foreach (var item in feed.Items)
                    {
                        if (!rssSoursesId.Any(x=>x.Equals(item.Categories)))
                        {

                            foreach (var item1 in item.Categories)
                            {
                                var a = item1.Name;
                            };


                            var newCategory = new Category()
                            {
                                Id = Guid.NewGuid(),
                               // Name = item.Categories,
                            };
                        }
                    }
                }



                if (feed.Items.Any())
                {
                    var currentNewsUrls = await _unitOfWork.News
                        .GetAll(false)//rssSourseId must be not nullable
                        .Select(n => n.Url)
                        .ToListAsync();

                    foreach (var syndicationItem in feed.Items)
                    {
                        if (!currentNewsUrls.Any(url => url.Equals(syndicationItem.Id)))
                        {
                            var newsDto = new NewsInfoFromRssSourseDto()
                            {
                                Id = Guid.NewGuid(),
                                RssSourseId = rssSourceModel.Id,
                                Url = syndicationItem.Id,
                                Title = syndicationItem.Title.Text,
                                Content = syndicationItem.Summary.Text //clean from html(?)
                            };
                            news.Add(newsDto);
                        }
                    }
                }

            }



            return news;

        }

        public void Save() => _unitOfWork.Save();
        public Task SaveAsync() => _unitOfWork.SaveAsync();
    }
}
