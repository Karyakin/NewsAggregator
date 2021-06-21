using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NewsAggregatorMain.Models;

namespace NewsAggregatorMain.TagHelpers
{
    public class PaginationTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public string SourseId { get; set; }

        [ViewContext]//контекст нашей вьюхи
        [HtmlAttributeNotBound]// в подстановке данного элемента не будет
        public ViewContext ViewContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">Содержимое тега</param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);// вернет УКЛ-хэлпер с которым мы будем работать
            var result = new TagBuilder("div");

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                var tag = new TagBuilder("a");//тег билдер создаст скобки автоматически
                var anchorInnerHtml = i.ToString();
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i, sourseId = SourseId });
                tag.InnerHtml.Append(anchorInnerHtml);
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
