using BankAPI.Models;
using Newtonsoft.Json;
using System.IO.Pipes;
using System.Net.Http;

namespace BankAPI.Services
{
    public static class GetInfo
    {
        private static HttpClient _httpClient = new HttpClient();

        public static async Task<IEnumerable<Rate>> GetRanks()
        {
            var serAdd = "https://developerhub.alfabank.by:8273/partner/1.0.1/public/rates/";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);
            // устанавливаем оба заголовка
            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await _httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseText);


            Bank lis = JsonConvert.DeserializeObject<Bank>(responseText);

            return lis.Rates;
        }

        public static async Task<IEnumerable<Currency>> GetCurrencies()
        {
            var serAdd = "https://api.nbrb.by/exrates/currencies";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);
            // устанавливаем оба заголовка
            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await _httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseText);


            IEnumerable<Currency> currencies = JsonConvert.DeserializeObject<IEnumerable<Currency>>(responseText);

            return currencies;
        }
    }
}
