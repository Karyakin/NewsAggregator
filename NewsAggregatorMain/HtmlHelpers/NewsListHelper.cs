using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace NewsAggregatorMain.HtmlHelpers
{
   /* public static HtmlString CreateListNews(this IHtmlHelper html,
            IEnumerable<NewsGetDTO> news)
    {
        var sb = new StringBuilder();

        foreach (var newsDto in news)
        {
            sb.Append($"<div><h2>{news.Article}</h2>");

            *//* GENERATE HTML 
             * <div>
                <h2>@Model.Article</h2>
                <div>
                    @Model.Body
                </div>
                <div>
                    <a asp-action="Details" asp-route-id="@Model.Id">Читать на аггрегаторе</a>
                </div>
                <div>
                    <a href="@Model.Url">Читать в источнике</a>
                </div>
            </div>
            <hr/>

             *//*
        }

        return new HtmlString(sb.ToString());
    }*/
}
