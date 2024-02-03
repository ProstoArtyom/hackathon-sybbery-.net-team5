using BankAPI.Models;
using Newtonsoft.Json;
using System.IO.Pipes;
using System.Net.Http;
using static System.Net.WebRequestMethods;

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

        public static async void GetNBCurName()
        {
            var serAdd = "https://api.nbrb.by/exrates/currencies";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);

            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await _httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();



            List<Currency> lis = JsonConvert.DeserializeObject<List<Currency>>(responseText);


        }
        public static async void GetNBCurName(string abbreviation)
        {
            var serAdd = "https://api.nbrb.by/exrates/currencies";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);

            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await _httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();



            List<Currency> lis = JsonConvert.DeserializeObject<List<Currency>>(responseText);
            int findId;
            foreach (Currency currency in lis)
            {
                if (currency.Cur_Abbreviation == abbreviation)
                {
                    findId = currency.Cur_ID;
                    break;
                }
            }


            var serverAdd = "https://api.nbrb.by/exrates/currencies";

            using var secondRequest = new HttpRequestMessage(HttpMethod.Get, serAdd);

            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var secResponse = await _httpClient.SendAsync(request);
            var secResponseText = await response.Content.ReadAsStringAsync();


        }
        public static async Task<NbrbRate> GetBBRates(int currencyId)
        {
            var serAdd = $"https://api.nbrb.by/exrates/rates/{currencyId}";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);

            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await _httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();

            var lis = JsonConvert.DeserializeObject<NbrbRate>(responseText);

            return lis;
        }

        public static async Task<List<NbrbRate>> GetBBRatesByDates(int currencyId, DateTime start, DateTime end)
        {
            var serAdd = $"https://api.nbrb.by/exrates/rates/dynamics/{currencyId}?startdate={start}&enddate={end}";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);

            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await _httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();

            var lis = JsonConvert.DeserializeObject<List<NbrbRate>>(responseText);

            return lis;
        }
    }
}
