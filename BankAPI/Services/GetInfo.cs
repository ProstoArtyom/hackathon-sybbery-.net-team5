using BankAPI.Models;
using Newtonsoft.Json;
using System.IO.Pipes;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace BankAPI.Services
{
    public class GetInfo
    {
        static HttpClient httpClient = new HttpClient();
        public async IAsyncEnumerable<string> GetNBCurNames()
        {
            var serAdd = "https://api.nbrb.by/exrates/currencies";

            using var request = new HttpRequestMessage(HttpMethod.Get, serAdd);
            // устанавливаем оба заголовка
            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "hello");

            using var response = await httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseText);


            List<CurrencyNB> lis = JsonConvert.DeserializeObject<List<CurrencyNB>>(responseText);

            foreach (CurrencyNB currency in lis)
            {
                yield return currency.Cur_Name_Eng;
            }
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
