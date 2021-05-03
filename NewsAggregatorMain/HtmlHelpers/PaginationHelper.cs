using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAggregatorMain.Models;
using System;
using System.Text;

namespace NewsAggregatorMain.HtmlHelpers
{
    public static class PaginationHelper
    {
        /// <summary>
        /// Класс предназначен для хранения разметки. Он сгинерит нам разметку
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageInfo"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public static HtmlString CreatePagination(this IHtmlHelper html,
            PageInfo pageInfo,
            Func<int, string> pageUrl)
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                var str = $"<a class=\"btn btn-default\" href={pageUrl(i)}> {i.ToString()}</a>";

                if (i == pageInfo.PageNumber)
                {
                    str = $"<a class=\"btn selected btn btn-primary\" href={pageUrl(i)}> {i.ToString()}</a>";
                }
                sb.Append(str);
            }

            return new HtmlString(sb.ToString());
        }
    }
}