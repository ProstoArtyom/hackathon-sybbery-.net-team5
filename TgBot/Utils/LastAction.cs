using TgBot.Enums;

namespace TgBot.Utils
{
    public class LastAction
    {
        public BankWorker Action { get; set; }
        public Data Data { get; set; } = new();
    }
}
