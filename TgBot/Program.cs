using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using TgBot.Handler;
using TgBot.Utils;

var bot = new TelegramBotClient("6925511900:AAFIkCLTgwRyPcS27u2avPs6GainQHHduao");

async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
}

var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = new UpdateType[]{UpdateType.Message}, // only recieve new messages
};

bot.StartReceiving(
    BaseHandler.HandleUpdateAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken
);

Console.WriteLine("Press enter to exit");
Console.ReadLine();

Console.WriteLine("Saving data to file");
await LastActionStorage.SaveToFile();