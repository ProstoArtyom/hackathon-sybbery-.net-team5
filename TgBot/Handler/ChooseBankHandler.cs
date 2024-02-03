using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Enums;
using TgBot.Utils;

namespace TgBot.Handler
{
    public class ChooseBankHandler
    {
        public static List<string> banks = new (){"АльфаБанк", "НБРБ"};
        public static List<string> carruncies = new (){"USD","GBP","EUR", "JPY"};
        
        public static async Task<bool> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var choosedBank = update.Message.Text;
            var storedClient = LastActionStorage.Storage[update.Message.Chat.Id];
            // get bank and check if it exists
            //var banks = BankService.GetBanks();

            // check stored value if returned form afterReate ))
            if(!string.IsNullOrEmpty(storedClient.Data.BankName) && banks.Contains(storedClient.Data.BankName))
                choosedBank = storedClient.Data.BankName;

            // if invalid bank, go to previous step
            if(!banks.Contains(choosedBank))
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Введен неверный банк, возможно, он находится в стадии разработки");
                storedClient.Action = BankWorker.Getbanks;
                return false;
            }

            // req bank currencies
            // carruncies = BankService.GetCurrenciues(choosedBank);
            if(carruncies.Count == 0)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "У банка нет валют");
                storedClient.Action = BankWorker.Getbanks;
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

            storedClient.Data = new(){BankName = choosedBank};
            storedClient.Action = BankWorker.Rate;
            return true;
        }
    }
}