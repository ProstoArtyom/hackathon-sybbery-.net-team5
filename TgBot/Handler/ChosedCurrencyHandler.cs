using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Enums;
using TgBot.Utils;

namespace TgBot.Handler
{
    public class ChosedCurrencyHandler
    {
        private static ReplyKeyboardMarkup menu = new(new[]
        {
            new KeyboardButton[] 
            {
                "Курс на текущий день",
                "Курс на выбранный день",
                "Собрать статистику",
            },
            new KeyboardButton[] 
            {
                "Выбрать другой банк",
                "Выбрать другую валюту"
            },
        }
        ){ResizeKeyboard = true};

       public static async Task<bool> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
       {
            var choosedCurrency = update.Message.Text;
            var storedClient = LastActionStorage.Storage[update.Message.Chat.Id];

            var bankName = storedClient.Data.BankName ?? "";

            // get bank currencies
            // bank currencies var bankCurr = BankService.GetCurr...(bankName);
            if(!ChooseCurrencyHandler.carruncies.Contains(choosedCurrency))
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Введен неправил курс, выберите другой");
                storedClient.Action = BankWorker.GetListCurrency;
                return false;
            }

            await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Выбранная валюта: {choosedCurrency}, Выбранный банк: {bankName}", replyMarkup: menu, cancellationToken: cancellationToken);

            storedClient.Action = BankWorker.AfterRate;
            storedClient.Data.Currency = choosedCurrency;
            return true;
       }
    }
}