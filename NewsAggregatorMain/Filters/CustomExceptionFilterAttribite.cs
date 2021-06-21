using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace NewsAggregatorMain.Filters
{
    /// <summary>
    /// при помощи фильтра исключений мы може сделать кастомный эксепшен и выбрасывать его на все однотипные ошибки, например красивая страничка 404, 
    /// чтобы не писать на все 300 контроллеров это исключение мы можен через стартап его внедрить на весь проект.
    /// 
    /// </summary>
    public class CustomExceptionFilterAttribite : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var action = context.ActionDescriptor.DisplayName;
            var vessage = context.Exception.Message;
            var stackTrace = context.Exception.StackTrace;
            var httpRequest = context.HttpContext.Request;

            context.Result = new ViewResult()
            {
                ViewName = "CustomError"
            };

        }
    }
}
