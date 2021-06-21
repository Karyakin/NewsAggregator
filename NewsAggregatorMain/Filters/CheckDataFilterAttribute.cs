using Contracts.ServicesInterfacaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Filters
{
    public class CheckDataFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly INewsService _newsService;
        public CheckDataFilterAttribute(INewsService newsService)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// Если QueryString будет содержать abc то в контроллер придет значение hiddenId
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isContains = context.HttpContext.Request.QueryString.Value?.Contains("abc");  //QueryString - набор параметров, которые будет содержать request это строка которая приходит в Url

            if (isContains.GetValueOrDefault())
            {
                context.ActionArguments["hiddenId"] = 42;
                context.ActionArguments["news"] = await _newsService.FindAllNews();
            }
            await next();
        }
    }
}
