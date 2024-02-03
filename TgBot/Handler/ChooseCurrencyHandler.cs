using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Enums;
using TgBot.Utils;

namespace TgBot.Handler
{
    public class ChooseCurrencyHandler
    {
        public static List<string> banks = new (){"БелИнвест", "Алфа", "БНБ"};
        public static List<string> carruncies = new (){"usd","rub","eur"};
        
        public static async Task<bool> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var choosedBank = update.Message.Text;
            var storagedClient = LastActionStorage.Storage[update.Message.Chat.Id];
            // get bank and check if it exists
            //var banks = BankService.GetBanks();

            // if invalid bank, go to previous step
            if(!banks.Contains(choosedBank))
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Введен неверный банк, введите другой еще раз");
                storagedClient.Action = BankWorker.Getbanks;
                return true;
            }

            // req bank currencies
            // carruncies = BankService.GetCurrenciues(choosedBank);
            if(carruncies.Count == 0)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "У банка нет валют");
                storagedClient.Action = BankWorker.Getbanks;
                return false;
            }

            var text = "Выберите нужную валюту";
            var keys = new List<KeyboardButton>();
            for(var i = 0; i < carruncies.Count; i++)
            {
                keys.Add(new KeyboardButton(carruncies[i]));
            }

            var ikm = new ReplyKeyboardMarkup(keys){ResizeKeyboard = true};
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, text, replyMarkup: ikm, cancellationToken: cancellationToken);

            storagedClient.Data.BankName = choosedBank;
            storagedClient.Action = BankWorker.Rate;
            return true;
        }
    }
}