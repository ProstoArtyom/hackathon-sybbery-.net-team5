using System.Security.Cryptography.X509Certificates;
using BankAPI.Services;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using TgBot.Utils;

namespace TgBot.Handler
{
    public class CurrentDayHandler
    {

        private static Dictionary<string, int> currs = new()
        {
            {"USD", 431},
            {"EUR", 451},
            {"GBP", 429},
            {"JPY", 508},
        };

        public static async Task<bool> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var storedClient = LastActionStorage.Storage[update.Message.Chat.Id];
            decimal curs = 0;
            try
            {
                switch(storedClient.Data.BankName)
                {
                    case "АльфаБанк":
                        curs = (await GetInfo.GetRanks()).FirstOrDefault()?.SellRate ?? 0;
                        break;
                    case "НБРБ":
                        curs = (decimal)(await GetInfo.GetBBRates(currs[storedClient.Data.Currency])).Cur_OfficialRate;
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to get currency for {storedClient.Data.BankName}:{storedClient.Data.Currency}; {ex.Message}");
            }

            await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"{storedClient.Data.BankName} - {storedClient.Data.Currency} {DateTime.Now.ToString("dd-MM-yyyy")}");
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Курс: {(curs == 0 ? "Курс недоступен" : curs)}");
            storedClient.Action = Enums.BankWorker.Rate;
            return false;
        }
    }
}