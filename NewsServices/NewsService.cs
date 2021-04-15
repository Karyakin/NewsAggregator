using AutoMapper;
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
        private readonly ICategoryService _categoryService;

        public NewsService(IUnitOfWork wrapper, IMapper mapper, ICategoryService categoryService)
        {
            _unitOfWork = wrapper;
            _mapper = mapper;
            _categoryService = categoryService;
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

            /* .ThenInclude(z => z.Category)*/
            //-- отсюда можно не делать, это тут не нужно и чисто для примера инклудов
            /*.ThenInclude(x => x.Comments)*/

            var news = await _unitOfWork.News.GetById(newsId.Value, false);

            return _mapper.Map<NewsGetDTO>(news);

        }

        public async Task<IEnumerable<NewsInfoFromRssSourseDto>> GetNewsInfoFromRssSourse(RssSourceModel rssSourceModel)
        {
            var news = new List<NewsInfoFromRssSourseDto>();
            var newCategories = new List<Category>();

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

                            try
                            {
                                if (!currentNewsUrls.Any(url => url.Equals(syndicationItem.Id)))
                                {
                                    var newsDto = new NewsInfoFromRssSourseDto()
                                    {
                                        Id = Guid.NewGuid(),
                                        RssSourseId = rssSourceModel.Id,
                                        Url = syndicationItem.Id,
                                        Title = syndicationItem.Title.Text,
                                        Content = syndicationItem.Summary.Text, //clean from html(?)

                                        CategoryId = (await _categoryService.FindCategoryByName(categoryName))?.Id
                                    };
                                    news.Add(newsDto);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error($"Something went wrong when trying to gave news from Message: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Something went wrong when trying to gave news from Message: {ex.Message}");
                }
            }
            return news;
        }

        public void Save() => _unitOfWork.Save();
        public Task SaveAsync() => _unitOfWork.SaveAsync();
    }
}
