using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.Enums;
using TgBot.Utils;

namespace TgBot.Handler
{
  public class AfterRateHandler
  {
    List<string> list = new List<string>() { "Курс на текущий день", "курс на выбранный день", "Собрать статистику"};

    public async static Task<bool> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
      switch(update.Message.Text)
      {
        case "Курс на текущий день":
          LastActionStorage.Storage[update.Message.Chat.Id].Action = BankWorker.CurrentDay;
          break;
        case "курс на выбранный день":
          LastActionStorage.Storage[update.Message.Chat.Id].Action = BankWorker.CustomDay;
          break;
        case "Собрать статистику":
          LastActionStorage.Storage[update.Message.Chat.Id].Action = BankWorker.Statistics;
          break;
        case "Выбрать другой банк":
          LastActionStorage.Storage[update.Message.Chat.Id].Action = BankWorker.Getbanks;
          break;
        case "Выбрать другую валюту":
          LastActionStorage.Storage[update.Message.Chat.Id].Action = BankWorker.GetListCurrency;
          break;

      }

      return false;
    }
  }
}
