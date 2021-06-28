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
using Services.Parsers;
using Services.ServiseHelpers;
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
        private readonly ICategoryService _categoryService;
        private readonly TutByParser _tutByParser;//Иная записть. Без интерфейса и применения базового типа. При такой записи нужно менять внедрение в Sturtup
        private readonly OnlinerParser _onlinerParser; //Иная записть. Без интерфейса и базового типа. При такой записи нужно менять внедрение в Sturtup
        private readonly IgromaniaParser _igromaniaParser; //Иная записть. Без интерфейса и базового типа. При такой записи нужно менять внедрение в Sturtup
        private readonly IRssSourceService _rssSourceService;
        private readonly IOONParser _OONParser;
        private readonly ITexterra _texterra;

        public NewsService(IUnitOfWork wrapper, IMapper mapper, ICategoryService categoryService, IRssSourceService rssSourceService,
            TutByParser tutByParser, OnlinerParser onlinerParser, IgromaniaParser igromaniaParser, OONParser oONParser, ITexterra texterra)
        {
            _unitOfWork = wrapper;
            _mapper = mapper;
            _categoryService = categoryService;
            _rssSourceService = rssSourceService;
            _tutByParser = tutByParser;
            _onlinerParser = onlinerParser;
            _igromaniaParser = igromaniaParser;
            _OONParser = oONParser;
            _texterra = texterra;
        }

        public async Task Aggregate()
        {

            var rsssouses = await _rssSourceService.GetAllRssSourceAsync(false);
            var newInfos = new List<NewsInfoFromRssSourseDto>(); // without any duplicate

            foreach (var item in rsssouses)
            {
                if (/*item.Name.Equals("TUT.by") ||*/ item.Name.Equals("Onliner") || item.Name.Equals("igromania") || item.Name.Equals("OON"))
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

        public void Delete(News news)
        {

            var newsEnt = _unitOfWork.News.GetByCondition(x => x.Id.Equals(news.Id), false).FirstOrDefault();
            try
            {
                _unitOfWork.News.Remove(newsEnt);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new Exception(e.Message);
            }

            _unitOfWork.SaveAsync();
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

            return getCompanyDTO;/*.OrderByDescending(x=>x.StartDate)*/
        }

        public async Task<NewsGetDTO> GetNewsBiId(Guid? newsId)
        {
            var news = await _unitOfWork.News.GetAll(false)
             .Include(x => x.Category)
             .Include(x => x.RssSource)
             .Include(x => x.Comments).ThenInclude(x => x.User).Where(x => x.Id.Equals(newsId.Value))
             .FirstOrDefaultAsync();

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

                            var document = await context.OpenAsync(req => req.Content(syndicationItem.Summary.Text));// для парсинга страницы

                            try
                            {
                                if (!currentNewsUrls.Any(url => url.Equals(syndicationItem.Id)))
                                {
                                    string lastText = null;
                                    string imageUrl = null;

                                    if (rssSourceModel.Name.Equals("TUT.by"))
                                    {
                                        lastText = (await _tutByParser.Parse(syndicationItem)).NewsText;
                                    }
                                    else if (rssSourceModel.Name.Equals("Onliner"))
                                    {
                                        var fullNewsText = (await _onlinerParser.Parse(syndicationItem));
                                        if (fullNewsText is null)
                                        {
                                            break;
                                        }
                                        lastText = fullNewsText.NewsText;
                                        imageUrl = fullNewsText.ImageUrl;
                                    }
                                    else if (rssSourceModel.Name.Equals("igromania"))
                                    {
                                        var fullNewsText = (await _igromaniaParser.Parse(syndicationItem));
                                        if (fullNewsText is null)
                                        {
                                            break;
                                        }
                                        lastText = fullNewsText.NewsText;
                                        imageUrl = fullNewsText.ImageUrl;
                                    }
                                    else if (rssSourceModel.Name.Equals("OON"))
                                    {
                                        var fullNewsText = (await _OONParser.Parse(syndicationItem));
                                        if (fullNewsText is null)
                                        {
                                            break;
                                        }
                                        lastText = fullNewsText.NewsText;
                                        imageUrl = fullNewsText.ImageUrl;
                                    }

                                    var newsDto = new NewsInfoFromRssSourseDto()
                                    {
                                        Id = Guid.NewGuid(),
                                        RssSourceId = rssSourceModel?.Id,
                                        Url = syndicationItem.Id,
                                        HeadImgUrl = imageUrl,
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

        public async Task RateNews()
        {
            var rateWorld = await _unitOfWork.RateWorld.GetAll(false).ToListAsync();
            var allNews = (await FindAllNews()).Where(date => date.EndDate is null).Take(29).ToArray();
            int newsRating = 0;
           
            for (int i = 0; i < 29; i++)
            {
                var model = await _texterra.GetTexterra(allNews[i].Summary);
                newsRating = GetNewsRating(model, rateWorld);

                allNews[i].Rating = newsRating;
                allNews[i].EndDate = DateTime.Now;
                _unitOfWork.News.Update(_mapper.Map<News>(allNews[i]));
            }

            await _unitOfWork.SaveAsync();
        }

        public int GetNewsRating(IEnumerable<Root> model, IEnumerable<RateWorlds> rateWorlds)
        {
            if (model is null)
            {
                return 0;
            }

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
        public void Save() => _unitOfWork.Save();
        public Task SaveAsync() => _unitOfWork.SaveAsync();
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
