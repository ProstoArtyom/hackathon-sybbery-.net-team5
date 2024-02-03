using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Utils;

namespace TgBot.Handler
{
    public class CurrentDayHandler
    {
        public static async Task<bool> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var storedClient = LastActionStorage.Storage[update.Message.Chat.Id];

            await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"{storedClient.Data.BankName} - {storedClient.Data.Currency} {DateTime.Now.ToString("dd-mm-yyyy")}");
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Курс: template");
            storedClient.Action = Enums.BankWorker.Rate;
            return false;
        }
    }
}