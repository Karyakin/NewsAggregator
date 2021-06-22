using Contracts.ParseInterface;
using Entities.Entity.NewsEnt;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Services.Parsers
{
    public class IgromaniaParser : IIgromaniaParser
    {
        public async Task<NewStrings> Parse(SyndicationItem syndicationItem)
        {
            var httpClient = new HttpClient();
            var request = await httpClient.GetAsync(syndicationItem.Id);
            var response = await request.Content.ReadAsStringAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='page_news noselect']");

            if (htmlBody is null)
            {
                return null;
            }


            int carrentItemIndex = 0;
            HtmlNode node;
            foreach (var item in htmlBody.ChildNodes)
            {
                if (item.OuterHtml.Contains("share_block") )
                {
                    carrentItemIndex++;
                }
            }
            node = htmlBody.ChildNodes[carrentItemIndex];
            node.Remove();

            var text = htmlBody.OuterHtml;

            var htmlDocPhotoUrl = new HtmlDocument();
            htmlDocPhotoUrl.LoadHtml(text);
            var htmlphoto = htmlDocPhotoUrl.DocumentNode.SelectSingleNode("//div[@class='main_pic_container']");

            if (htmlphoto is null)
            {
                return null;
            }

            var photoUrlDirty = htmlphoto.OuterHtml;

            int newsHendlerImageUrlStart = photoUrlDirty.IndexOf("\"https:");
            int newsHendlerImageUrlEnd = photoUrlDirty.IndexOf(".jpg");

            string imageUrl = photoUrlDirty.Substring(newsHendlerImageUrlStart + 1, newsHendlerImageUrlEnd + 3 - newsHendlerImageUrlStart);


            var fullNewsText = new NewStrings
            {
                ImageUrl = imageUrl,
                NewsText = text
            };


            return fullNewsText;

        }
    }
}
/*||
                    item.OuterHtml.Equals("info_block_botrt") ||
                    item.OuterHtml.Equals("uninote console") ||
                    item.OuterHtml.Equals("clear_block clearfix")*/