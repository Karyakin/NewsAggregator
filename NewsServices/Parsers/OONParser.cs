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
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='main-container container']");

            var newsText = htmlBody.OuterHtml;



            var htmlDocWitout = new HtmlDocument();
            htmlDocWitout.LoadHtml(newsText);
            var htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='col-md-4 col-xs-12 top-right-column']");
            if (htmlBodyWitout is null)
            {
                return null;
            }
            htmlBodyWitout.RemoveAllChildren();



            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//aside[@class='col-sm-3 sidebar sidebar-second']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='panel panel-info']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='field-items']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='field-item even']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//section[@id='block-views-story-story-tags-block']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }


            var text = htmlDocWitout.DocumentNode.OuterHtml;

            #region PhotoUrl

            var htmlDocPhotoUrl = new HtmlDocument();
            htmlDocPhotoUrl.LoadHtml(text);
            var htmlphoto = htmlDocPhotoUrl.DocumentNode.SelectSingleNode("//img[@class='img-responsive']");

            if (htmlphoto is null)
            {
                return null;
            }

            var photoUrlDirty = htmlphoto.OuterHtml;

            int newsHendlerImageUrlStart = photoUrlDirty.IndexOf("\"https:");
            int newsHendlerImageUrlEnd = photoUrlDirty.IndexOf(".jpg\"");

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
