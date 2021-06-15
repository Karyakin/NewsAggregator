using AngleSharp;
using AutoMapper;
using Contracts.ParseInterface;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Serilog;
using Services.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly TutByParser _tutByParser;//Иная записть. Без интерфейса и применения базового типа. При такой записи нужно менять внедрение в Sturtup
        private readonly OnlinerParser _onlinerParser; //Иная записть. Без интерфейса и базового типа. При такой записи нужно менять внедрение в Sturtup
        private readonly IRssSourceService _rssSourceService;
        /*private readonly ITutByParser _tutByParser;
        private readonly IOnlinerParser _onlinerParser;*/

        public NewsService(IUnitOfWork wrapper, IMapper mapper, ICategoryService categoryService, IRssSourceService rssSourceService, TutByParser tutByParser, OnlinerParser onlinerParser)
        {
            _unitOfWork = wrapper;
            _mapper = mapper;
            _categoryService = categoryService;
            _rssSourceService = rssSourceService;
            _tutByParser = tutByParser;
            _onlinerParser = onlinerParser;
        }

        public async Task Aggregate()
        {

            var rsssouses = await _rssSourceService.GetAllRssSourceAsync(false);
            var newInfos = new List<NewsInfoFromRssSourseDto>(); // without any duplicate

            foreach (var item in rsssouses)
            {
                if (/*item.Name.Equals("TUT.by") ||*/ item.Name.Equals("Onliner"))
                {
                    var newsList = await GetNewsInfoFromRssSourse(item);
                    newInfos.AddRange(newsList);
                }
            };

            await CreateManyNewsAsync(newInfos);
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
                                        Authors = syndicationItem.Authors.Select(x => x.Name),
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

        public async Task RateNews()
        {
            /*  string myNews = "счастье хорошо жить ура любовь";*/
            var rateWorld = await _unitOfWork.RateWorld.GetAll(false).ToListAsync();

            var allNews = (await FindAllNews()).Where(date => date.EndDate is null).ToArray();

            int newsRating = 0;

            for (int i = 0; i < 14; i++)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=c2d840e08cb29f30a623a3c3530d4152fd158188")
                    {
                        Content = new StringContent("[{\"text\":\"" + /*myNews*/ allNews[i].Summary + "\"}]",

                            Encoding.UTF8,
                            "application/json")
                    };
                    var response = await httpClient.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();


                    var model = JsonConvert.DeserializeObject<IEnumerable<Root>>(responseString);

                    if (model is null)
                    {
                        Log.Error("Impossible to get a response from the server \"http://api.ispras.ru/texterra\"");
                        throw new ArgumentNullException("Impossible to get a response from the server \"http://api.ispras.ru/texterra\"");
                    }

                    newsRating =  GetNewsRating(model, rateWorld);
                }

                if (allNews[i].EndDate is null)
                {
                    allNews[i].Rating = newsRating;
                    allNews[i].EndDate = DateTime.Now;
                }

                _unitOfWork.News.Update(_mapper.Map<News>(allNews[i]));
            }

            await _unitOfWork.SaveAsync();
        }

        public int GetNewsRating(IEnumerable<Root> model, IEnumerable<RateWorlds> rateWorlds)
        {
            List<string> worldsInNews = new List<string>();

            int rateForNews = 0;
            foreach (var item in model)
            {
                var lemma = item.annotations.lemma;
                foreach (var lemmaItem in lemma)
                {
                    worldsInNews.Add(lemmaItem.value);
                }
            }

            foreach (var rate in rateWorlds)
            {
                foreach (var world in worldsInNews)
                {
                    if (rate.Name == world)
                    {
                        rateForNews += rate.Value;
                    }
                }
            }
            return rateForNews;
        }
    }

    #region Clase for serialazer json https://json2csharp.com/
    public class Lemma
    {
        public int start { get; set; }
        public int end { get; set; }
        public string value { get; set; }
    }

    public class Annotations
    {
        public List<Lemma> lemma { get; set; }
    }

    public class Root
    {
        public string text { get; set; }
        public Annotations annotations { get; set; }
    }
    #endregion
}
