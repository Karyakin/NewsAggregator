using AutoMapper;
using Contracts;
using Contracts.ServicesInterfacaces;
using Entities;
using Entities.CurrencyExchange;
using Entities.DataTransferObject;
using Microsoft.Extensions.Options;
using Serilog;
using Services.СurrencyExchangeHelpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Если бы было много сервисов, я бы создал класс типа "Wrapper" и через конструкторы засунул бы в тот Wrapper все сервисы до кучи, 
    /// чтобы в каждом сервисе не обращатся к HttpClientFactory и не дергать IOptions для получения url.
    /// </summary>
    public class ExchangeService : IExchangeService
    {
        public HttpClient _httpClient;
        private readonly IOptions<NationalBankSettings> _nationalBankConfig;
        private readonly IMapper _mapper;
       

        public ExchangeService(HttpClient httpClient, IOptions<NationalBankSettings> options, IMapper mapper)
        {
            _httpClient = httpClient;
            _nationalBankConfig = options;
            _mapper = mapper;
          
        }

        public async Task<IEnumerable<CurrencyDto>> GetCurrencyExchangeAsync(DateTime? onDate, int? periodicity)
        {
            if (!onDate.HasValue || onDate.Value.AddDays(1) > onDate)//onDate.Value.AddDays(1) > onDate - такую проверку я бы не проводил, т.к. это может ввести в заблуждения пользователя(если он уже ввел данные на завтрашний день, то он и не будет смотреть, что ввел не то), но для нагладности проверил
                onDate = DateTime.Now;
            try
            {
                Log.Information($"Обращение к контроллеру ExchangeService, дата:{DateTime.Now}");
                if (!periodicity.HasValue || periodicity.Value < 0 || periodicity.Value > 1)
                    periodicity = 0;
             
                var str = $"{_nationalBankConfig.Value.BaseUrl}exrates/rates?ondate={onDate.Value.ToString("yyyy-MM-dd")}&periodicity={periodicity}";
                var responseString = await _httpClient.GetStringAsync(str);
                var catalogCurrencyDto = JsonSerializer.Deserialize<IEnumerable<CurrencyDto>>(responseString);


                return catalogCurrencyDto;
            }
            catch (Exception ex)
            {
                Log.Error($"При попытке получения курсов валют произошла ошибка. Подробности " +
                   $"{ex.Message},\n {ex.Source}, \n {ex.HelpLink}");
                return null;
            }
        }
    }
}
