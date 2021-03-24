using Contracts.WrapperInterface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class NewsController : Controller
    {
        public IRepositoryWrapper _wrapper;
        public NewsController(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        //[HttpGet]
        public IActionResult Index()
        {

            //var resq = _wrapper.News.FindByCondition(6376438,false);
            var res = _wrapper.News.FindAll(trackChanges: false).ToList();


            return Ok(res);
        }
    }
}
