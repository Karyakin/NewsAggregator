﻿using AngleSharp;
using Contracts.ParseInterface;
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
        public async Task<string> Parse(SyndicationItem syndicationItem)
        {


            /*  var httpClient = new HttpClient();
               var request = await httpClient.GetAsync(syndicationItem.Id);
               var response = await request.Content.ReadAsStringAsync();

               #region CutHendlerImage

               int newsHendlerImageStart = response.IndexOf("<div class=\"news-header__image\"");// еогда нет заголовочной фотки вылетаем

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

               string text = listGroup.Replace("class=\"news-text\">", "");

               return text;*/


            var html = @"https://auto.onliner.by/2021/04/27/v-minsk-privezli-trexdvernyj-land-rover-defender-90-za-korotysha-prosyat-1956-tysyachi-rublej";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class=\"news-reference\"]");

            HtmlNode node11 = node.ChildNodes[1];

            node11.Remove();





            return "";







        }
    }
}
