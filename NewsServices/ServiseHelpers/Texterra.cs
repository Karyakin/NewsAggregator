using Newtonsoft.Json;
using Serilog;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiseHelpers
{
    public class Texterra : ITexterra
    {

        public async Task<IEnumerable<Root>> GetTexterra(string summary)
        {
            string responseString = null;
            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=913cdfa8a00e017c443ea0bf209204343e26975c")
                {
                    Content = new StringContent("[{\"text\":\"" + summary + "\"}]",

                    Encoding.UTF8,
                    "application/json")
                };
                var response = await httpClient.SendAsync(request);
                responseString = await response.Content.ReadAsStringAsync();

                /*if (string.IsNullOrEmpty(responseString))
                {
                    Log.Information($"не удалось оценить новость {DateTime.Now}.\n Сообщение от сервера:\n " +
                        $"StatusCode: {response.StatusCode}, " +
                        $"Headers: {response.Headers}, Content: {response.Content}, " +
                        $"RequestMessage: {response.RequestMessage}");
                    throw new ArgumentNullException("\n______________________________________________________________________________");
                }*/
            }

            var model = JsonConvert.DeserializeObject<IEnumerable<Root>>(responseString);
            return model;
        }
    }
}
