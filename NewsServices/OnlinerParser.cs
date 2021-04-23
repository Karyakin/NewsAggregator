using AngleSharp;
using Contracts.ParseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OnlinerParser : IWebPageParser
    {
        public async Task<string> Parse()
        {

            var url = "https://news.tut.by/rss/all.rss";
            var url1 = "https://news.tut.by/economics/727719.html";

            //Use the default configuration for AngleSharp
            var config = Configuration.Default;

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            var httpClient = new HttpClient();
            var request = await httpClient.GetAsync(url1);


            var response = await request.Content.ReadAsStringAsync();


            var config1 = Configuration.Default.WithDefaultLoader();
            var context1 = BrowsingContext.New(config1);
            var document1 = await context1.OpenAsync(url1);

            return "";
        }
    }
}
