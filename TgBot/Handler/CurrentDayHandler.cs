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
            {"USD", 145},
            {"EUR", 19},
            {"GBP", 143},
            {"JPY", 67},
        };

        public static async Task<bool> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var storedClient = LastActionStorage.Storage[update.Message.Chat.Id];
            decimal curs = 0;
            switch(storedClient.Data.BankName)
            {
                case "АльфаБанк":
                    curs = (await GetInfo.GetRanks()).FirstOrDefault()?.SellRate ?? 0;
                    break;
                case "НБРБ":
                    curs = (decimal)(await GetInfo.GetBBRates(currs[storedClient.Data.Currency])).Cur_OfficialRate;
                    break;
            }
            

            await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"{storedClient.Data.BankName} - {storedClient.Data.Currency} {DateTime.Now.ToString("dd-MM-yyyy")}");
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Курс: {(curs == 0 ? "Курс недоступен" : curs)}");
            storedClient.Action = Enums.BankWorker.Rate;
            return false;
        }
    }
}