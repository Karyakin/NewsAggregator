using AngleSharp;
using Contracts.ParseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class OnlinerParser : IOnlinerParser
    {
        public async Task<string> Parse(SyndicationItem syndicationItem)
        {
            var httpClient = new HttpClient();
            var request = await httpClient.GetAsync(syndicationItem.Id);
            var response = await request.Content.ReadAsStringAsync();

            #region CutHendlerImage
            


            int newsHendlerImageStart = response.IndexOf("<div class=\"news-header__image\"");// еогда нет заголовочной фотки вылетаем
            
            
            string newsHendlerImageStartToEnd = response.Substring(newsHendlerImageStart);
            int newsHendlerImageEnd = newsHendlerImageStartToEnd.IndexOf("</div>");
            string newsHendlerImage = $"{newsHendlerImageStartToEnd.Substring(0, newsHendlerImageEnd)}";

            int newsHendlerImageUrlStart = newsHendlerImage.IndexOf("'");
            int newsHendlerImageUrlEnd = newsHendlerImage.LastIndexOf("'");

            string imageUrl = newsHendlerImage.Substring(newsHendlerImageUrlStart + 1, newsHendlerImageUrlEnd - 1 - newsHendlerImageUrlStart);
            var imageUrlTeg = $"<img loading=\"lazy\" class=\"alignnone size-820x5616 wp-image-867035 news-media__image\" src=\"{imageUrl}\">";

            #endregion


            int start = response.IndexOf("class=\"news-text\"");
            string startEnd = response.Substring(start);

            int end = startEnd.Contains("<hr />")
                ? startEnd.IndexOf("<hr />")
                : startEnd.IndexOf("<hr>");

            if (end is -1)
            {
                end = startEnd.IndexOf("news-grid__part news-grid__part_2 news-helpers_hide_tablet");
            }


            string listGroup = startEnd.Substring(0, end);

            listGroup = $"{imageUrlTeg} {listGroup}";

            string lastTekst = listGroup.Replace("class=\"news-text\">", "");

            return  lastTekst;






        }
    }
}
