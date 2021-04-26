using Contracts.ParseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Services.Parsers
{
   public class TutByParser : ITutByParser
    {
        public async Task<string> Parse(SyndicationItem syndicationItem)
        {
            var httpClient = new HttpClient();
            var request = await httpClient.GetAsync(syndicationItem.Id);
            var response = await request.Content.ReadAsStringAsync();
            int start = response.IndexOf("<div id=\"article_body\"");
            string startEnd = response.Substring(start);

            int end = startEnd.Contains("!--POLL--")
                ? startEnd.IndexOf("!--POLL--")
                : startEnd.IndexOf("<div class");

            string listGroup = startEnd.Substring(0, end);
            var lastText = listGroup
               .Replace("&nbsp;", " ")
               .Replace("&mdash;", " ")
               .Replace("&amp;", " ")
               .Replace("&nbsp;", " ")
               .Replace("&laquo;", " ")
               .Replace("&raquo;", " ");
            
            return lastText;
        }
    }
}
