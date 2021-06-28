using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Entities.Models.AssembledModel;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace NewsAggregatorMain.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class RssSoursesController : Controller
    {
        private readonly IRssSourceService _rssSourceService;
        private readonly IUnitOfWork _unitOfWork;
        public RssSoursesController(IRssSourceService rssSourceService, IUnitOfWork unitOfWork)
        {
            _rssSourceService = rssSourceService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var rssSource = await _rssSourceService.GetAllRssSourceAsync(false);
            return View(rssSource);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sourse = await _rssSourceService.GetRssSourceById(id);

            if (sourse == null)
            {
                return NotFound();
            }
            return View(sourse);
        }

        public async Task<IActionResult> DetailsWithNews(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sourse = await _rssSourceService.RssSourceByIdWithNews(id);

            if (sourse == null)
            {
                return NotFound();
            }

            return View(sourse);
        }

        public IActionResult Create(RssSource rssSource)
        {
            return  View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RssSourceModel rssSourceModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var reader = XmlReader.Create(rssSourceModel.Url))
                    {
                        var feed = SyndicationFeed.Load(reader);
                        reader.Close();
                    }
                    await _rssSourceService.CreateOneRssSource(rssSourceModel);
                }
                catch (Exception ex)
                {
                    Log.Error($"Something went wrong when trying to add to the source. Message: {ex.Message}");
                    return BadRequest( $"Something went wrong when trying to add to the source. Incorrect Rss Sourse adress!");
                }
            }
            return  RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteNews(SourseWithNewsCategory sourseWithNewsCategory)
        {
            var news = _unitOfWork.News.GetByCondition(x => x.Id.Equals(sourseWithNewsCategory.Id), false).SingleOrDefault();
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
            return RedirectToAction("DetailsWithNews", new { id = news.RssSourceId });
        }
    }
}
