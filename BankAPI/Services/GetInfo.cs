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

        public async void GetNBCurName()
        {
            var serAdd = "https://api.nbrb.by/exrates/currencies";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);

            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();



            List<CurrencyNB> lis = JsonConvert.DeserializeObject<List<CurrencyNB>>(responseText);


        }
        public async void GetNBCurName(string abbreviation)
        {
            var serAdd = "https://api.nbrb.by/exrates/currencies";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);

            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();



            List<CurrencyNB> lis = JsonConvert.DeserializeObject<List<CurrencyNB>>(responseText);
            int findId;
            foreach (CurrencyNB currency in lis)
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

            using var secResponse = await httpClient.SendAsync(request);
            var secResponseText = await response.Content.ReadAsStringAsync();


        }
        public async Task<NbrbRate> GetBBRates(int currencyId)
        {
            var serAdd = $"https://api.nbrb.by/exrates/rates/{currencyId}";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);

            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();

            var lis = JsonConvert.DeserializeObject<NbrbRate>(responseText);

            return lis;
        }
    }
}
