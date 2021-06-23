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
    public class OONParser : IOONParser
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
            var newsText = htmlBody.OuterHtml;



            var htmlDocWitout = new HtmlDocument();
            htmlDocWitout.LoadHtml(newsText);
            var htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='share_block']");
            if (htmlBodyWitout is null)
            {
                return null;
            }
            htmlBodyWitout.RemoveAllChildren();

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='uninote console']");


            if (htmlBodyWitout is null)
            {
                return null;
            }
            htmlBodyWitout.RemoveAllChildren();



            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='share_block']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='favorite_block']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }


            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='page_news_info clearfix']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }


            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='nepncont']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }



            var text = htmlDocWitout.DocumentNode.OuterHtml;

            #region PhotoUrl

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
            #endregion

            var fullNewsText = new NewStrings
            {
                ImageUrl = imageUrl,
                NewsText = text
            };


            return fullNewsText;



        }
    }
}
