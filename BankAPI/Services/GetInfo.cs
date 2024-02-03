using BankAPI.Models;
using Newtonsoft.Json;
using System.IO.Pipes;
using System.Net.Http;

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
    }
}
