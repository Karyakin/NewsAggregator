using AngleSharp;
using AutoMapper;
using Contracts.ParseInterface;
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
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly ITutByParser _tutByParser;
        private readonly IOnlinerParser _onlinerParser;

        public NewsService(IUnitOfWork wrapper, IMapper mapper, ICategoryService categoryService, ITutByParser tutByParser, IOnlinerParser onlinerParser)
        {
            _unitOfWork = wrapper;
            _mapper = mapper;
            _categoryService = categoryService;
            _tutByParser = tutByParser;
            _onlinerParser = onlinerParser;
        }

        public async Task CreateManyNewsAsync(IEnumerable<NewsInfoFromRssSourseDto> newsInfoFromRssSourseDtos)
        {
            try
            {
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
            _unitOfWork.News.Add(news);
            await _unitOfWork.SaveAsync();
        }

        public Task<NewsInfoFromRssSourseDto> Delete()
        {
            throw new NotImplementedException();
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


            var rssSourseWithNews = await _unitOfWork.News.GetByCondition(x => x.Id.Equals(newsId), true)
               .Include(x => x.Category)
               .Include(x => x.RssSource)
               .SingleOrDefaultAsync();

            var news = await _unitOfWork.News.GetById(newsId.Value, false);

            var test = _mapper.Map<NewsGetDTO>(news);

            return test;
        }

        public async Task<IEnumerable<NewsInfoFromRssSourseDto>> GetNewsInfoFromRssSourse(RssSourceModel rssSourceModel)
        {
            var news = new List<NewsInfoFromRssSourseDto>();
            var newCategories = new List<Category>();

            //Use the default configuration for AngleSharp
            var config = Configuration.Default;// для парсинга страницы

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);// для парсинга страницы

            using (var reader = XmlReader.Create(rssSourceModel.Url))
            {
                var feed = SyndicationFeed.Load(reader);
                reader.Close();

                newCategories = await _categoryService.CheckCategoriesForDublication(feed);// чекаем дубликаты
                await _categoryService.CreateManyCategories(newCategories);//после того, как мы чекнули название категорий, те, которых нет в базе туда заносятся

                try
                {
                    if (feed.Items.Any())
                    {
                        foreach (var syndicationItem in feed.Items)
                        {
                            string categoryName = null;
                            foreach (var category in syndicationItem.Categories)
                            {
                                categoryName = category.Name;
                            }

                            var currentNewsUrls = await _unitOfWork.News
                            .GetAll(false)//rssSourseId must be not nullable
                            .Select(n => n.Url)
                            .ToListAsync();

                            //Create a virtual request to specify the document to load (here from our fixed string)
                            var document = await context.OpenAsync(req => req.Content(syndicationItem.Summary.Text));// для парсинга страницы
                            // var title = document.DocumentElement.TextContent;


                            try
                            {
                                if (!currentNewsUrls.Any(url => url.Equals(syndicationItem.Id)))
                                {

                                    #region MyRegion
                                    /*var httpClient = new HttpClient();
                                   var request = await httpClient.GetAsync(syndicationItem.Id);
                                   var response = await request.Content.ReadAsStringAsync(); 
                                   int start = response.IndexOf("<div id=\"article_body\"");
                                   string startEnd = response.Substring(start);

                                   int end = startEnd.Contains("!--POLL--") 
                                       ? startEnd.IndexOf("!--POLL--") 
                                       : startEnd.IndexOf("<div class");

                                   string listGroup = startEnd.Substring(0, end);
                                   var lastText = listGroup
                                      .Replace("&nbsp;", " ")
                                      .Replace("&mdash;", " ")
                                      .Replace("&amp;", " ")
                                      .Replace("&nbsp;", " ")
                                      .Replace("&laquo;", " ")
                                      .Replace("&raquo;", " ");*/
                                    #endregion

                                    string lastText = null;

                                    if (rssSourceModel.Name.Equals("TUT.by"))
                                    {
                                         lastText = await _tutByParser.Parse(syndicationItem);
                                    }
                                    else if (rssSourceModel.Name.Equals("Onliner"))
                                    {
                                        lastText = await _onlinerParser.Parse(syndicationItem);
                                    }


                                    var newsDto = new NewsInfoFromRssSourseDto()
                                    {
                                        Id = Guid.NewGuid(),
                                        RssSourceId = rssSourceModel?.Id,
                                        Url = syndicationItem.Id,
                                        Title = syndicationItem.Title.Text,
                                        Summary = document.DocumentElement.TextContent, //syndicationItem.Summary.Text, //clean from html(?)
                                        Authors = syndicationItem.Authors.Select(x=>x.Name),
                                        Body = lastText,
                                        CategoryId = (await _categoryService.FindCategoryByName(categoryName))?.Id
                                    };
                                    news.Add(newsDto);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error($"Something went wrong when trying to gave news from {rssSourceModel.Name}. Message: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Something went wrong when trying to gave news from {rssSourceModel.Name}. Message: {ex.Message}");
                }
            }
            return news;
        }

        public void Save() => _unitOfWork.Save();
        public Task SaveAsync() => _unitOfWork.SaveAsync();
    }
}
