using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace NewsAggregatorMain.Filters
{
    public class ChromFilterAttribute : Attribute, IResourceFilter
    {
        /// <summary>
        /// будет выполнен после фильтра авторизации, но до выполенения методов  акшин фильтров, фильтров исключений и фильтров результатов
        /// </summary>
        /// <param name="context">переметр который позволит получить данные запроса и взаимодействоват ьс ответом</param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            if (!userAgent.Contains("Chrome/"))
            {
                context.Result = new RedirectToActionResult("Index", "News", null);// если нужно перекинуть в метод
            }
        }

        /// <summary>
        /// заход в filter pipeline на обратном пути(перед отдачей результата)
        /// Можно делать туже любую логику, но отрабатывать она будет при возвоате запроса. Т.е. теперь ответ не пойдет в хроме
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            context.Result = new RedirectToActionResult("Index", "News", null);// если нужно перекинуть в метод
        }
    }
}
