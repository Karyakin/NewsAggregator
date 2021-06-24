using AngleSharp;
using Contracts.ParseInterface;
using Entities.Entity.NewsEnt;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Services
{
    public class OnlinerParser : IOnlinerParser
    {
        public async Task<NewStrings> Parse(SyndicationItem syndicationItem)
        {
            /* var httpClient = new HttpClient();
             var request = await httpClient.GetAsync(syndicationItem.Id);
             var response = await request.Content.ReadAsStringAsync();

             #region CutHendlerImage

             int newsHendlerImageStart = response.IndexOf("<div class=\"news-header__image\"");

             string newsHendlerImageStartToEnd = response.Substring(newsHendlerImageStart);
             int newsHendlerImageEnd = newsHendlerImageStartToEnd.IndexOf("</div>");
             string newsHendlerImage = $"{newsHendlerImageStartToEnd.Substring(0, newsHendlerImageEnd)}";

             int newsHendlerImageUrlStart = newsHendlerImage.IndexOf("(");
             int newsHendlerImageUrlEnd = newsHendlerImage.LastIndexOf(")");

             string imageUrl = newsHendlerImage.Substring(newsHendlerImageUrlStart + 1, newsHendlerImageUrlEnd - 1 - newsHendlerImageUrlStart);

             imageUrl = imageUrl.Replace("'", "");
             var imageUrlTeg = $"<img loading=\"lazy\" class=\"alignnone size-820x5616 wp-image-867035 news-media__image\" src=\"{imageUrl}\">";

             #endregion

             int start = response.IndexOf("class=\"news-text\"");
             string startEnd = response.Substring(start);

             int end = startEnd.IndexOf("news-grid__part news-grid__part_2 news-helpers_hide_tablet");

             string listGroup = startEnd.Substring(0, end);

             listGroup = $"{imageUrlTeg} {listGroup}";

             string text = listGroup.Replace("class=\"news-text\">", "");*/

            var httpClient = new HttpClient();
            var request = await httpClient.GetAsync(syndicationItem.Id);
            var response = await request.Content.ReadAsStringAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='news-posts']");

            var newsText = htmlBody.OuterHtml;



            var htmlDocWitout = new HtmlDocument();
            htmlDocWitout.LoadHtml(newsText);
            var htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='social-likes news-reference__control']");
            if (htmlBodyWitout is null)
            {
                return null;
            }
            htmlBodyWitout.RemoveAllChildren();



            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//a[@href='https://catalog.onliner.by/furnituresafes?safetype%5B0%5D=seifbook&safetype%5Boperation%5D=union']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-widget']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//p[@style='text-align: right;']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//p[@style='text-align: right;']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-reference']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-reference']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }
            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-reference']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-reference__list']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//section[@id='news-reference__list']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-header__control']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }


            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-grid__part news-grid__part_2 news-helpers_hide_tablet js-banner-container']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-reference']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }



            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-media news-media_condensed']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }


            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-reference']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//h2");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//hr");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }


            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//strong");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }


            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-reference__author news-helpers_show_mobile']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }

            htmlBodyWitout = htmlDocWitout.DocumentNode.SelectSingleNode("//div[@class='news-reference']");
            if (htmlBodyWitout != null)
            {
                htmlBodyWitout.RemoveAllChildren();
            }



            var text = htmlDocWitout.DocumentNode.OuterHtml;

            #region PhotoUrl

            var htmlDocPhotoUrl = new HtmlDocument();
            htmlDocPhotoUrl.LoadHtml(text);
            var htmlphoto = htmlDocPhotoUrl.DocumentNode.SelectSingleNode("//div[@class='news-header__image']");

            if (htmlphoto is null)
            {
                return null;
            }

            var photoUrlDirty = htmlphoto.OuterHtml;

            int newsHendlerImageUrlStart = photoUrlDirty.IndexOf("\'https:");
            int newsHendlerImageUrlEnd = photoUrlDirty.IndexOf(".jpeg\'");
            if (newsHendlerImageUrlEnd ==-1)
            {
                newsHendlerImageUrlEnd = photoUrlDirty.IndexOf(".jpg\'");
            }

            string imageUrl = photoUrlDirty.Substring(newsHendlerImageUrlStart + 1, newsHendlerImageUrlEnd + 4 - newsHendlerImageUrlStart);
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
