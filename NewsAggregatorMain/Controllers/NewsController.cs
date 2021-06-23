using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAggregatorMain.Models;
using NewsAggregatorMain.Models.ViewModel.NewsVM;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    //[Authorize(Policy = "18+Content")]
    [Authorize(Roles = "Admin, User")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRssSourceService _rssSourceService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IExchangeService _exchangeService;
        public NewsController(INewsService newsService, IUnitOfWork unitOfWork,
                              IRssSourceService rssSourceService, ICommentService commentService, 
                              IMapper mapper, IExchangeService exchangeService)
        {
            _newsService = newsService;
            _unitOfWork = unitOfWork;
            _rssSourceService = rssSourceService;
            _commentService = commentService;
            _mapper = mapper;
            _exchangeService = exchangeService;
        }

        public IActionResult Aggregate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Aggregate(CreateNewsViewModel createNewsViewModel)
        {
            var rsssouses = await _rssSourceService.GetAllRssSourceAsync(false);
            var newInfos = new List<NewsInfoFromRssSourseDto>(); // without any duplicate

            foreach (var item in rsssouses)
            {
                if (/*item.Name.Equals("TUT.by") || item.Name.Equals("Onliner")*/ item.Name.Equals("igromania") /*|| item.Name.Equals("OON")*/)
                {
                    var newsList = await _newsService.GetNewsInfoFromRssSourse(item);
                    newInfos.AddRange(newsList);
                }
            };

            await _newsService.CreateManyNewsAsync(newInfos);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var currence = await _exchangeService.GetCurrencyExchangeAsync(DateTime.Now, 0);

            foreach (var item in currence)
            {
                var curAbbreviation = item.CurAbbreviation;
                var currencyValue = item.CurOfficialRate;
                if (curAbbreviation.Equals("USD"))
                {
                    ViewData["USD"] = currencyValue;
                }
                if (curAbbreviation.Equals("EUR"))
                {
                    ViewData["EUR"] = currencyValue;
                }
                if (curAbbreviation.Equals("RUB"))
                {
                    ViewData["RUB"] = $"{currencyValue} за 100 росс.рублей";
                }
            }

            var allNews = (await _newsService.FindAllNews()).ToList();

            var pageSize = 9;
            var newsPerPages = allNews.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = allNews.Count
            };

            return View(new NewsListWithPaginationInfo()
            {
                News = newsPerPages,
                PageInfo = pageInfo,
                IsMember = HttpContext.User.Claims.Any(x => x.Value.Contains("Admin"))
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsViewModel createNewsViewModel)
        {
            var a = await _rssSourceService.GetAllRssSourceAsync(false);//

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateNewsViewModel()
            {
                Sources = new SelectList(await _rssSourceService.GetAllRssSourceAsync(false),
               "Id",
               "Name")
            };

            return View(model);
        }

        public async Task<IActionResult> Details(NewsGetDTO newsGetDTO)
        {
            var newsWithDetails = await _newsService.GetNewsBiId(newsGetDTO.Id);
            return View(newsWithDetails);
        }
             
        public async Task<IActionResult> ReadInAgregator(NewsGetDTO newsWithCommentsDTO)
        {
            var newsWithDetails = await _newsService.GetNewsBiId(newsWithCommentsDTO.Id);
            var commentsEnt = await _commentService.FindAllCommentsForNews(newsWithCommentsDTO.Id);
            var comentsDto = _mapper.Map<IEnumerable<CommentDto>>(commentsEnt);
            newsWithDetails.Comments = comentsDto;
         
            return View(newsWithDetails);
        }

       public async Task<IActionResult> DeleteNews(NewsGetDTO newsGetDTO)
        {
           var news = _unitOfWork.News.GetByCondition(x => x.Id.Equals(newsGetDTO.Id), false).SingleOrDefault();
            if (news is null)
            {
                BadRequest("Can't find news!");
            }

            try
            {
                _unitOfWork.News.Remove(news);
            }
            catch (Exception ex)
            {

                Log.Error($"something went wrong. Details: {ex.Message}");
            }

            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
