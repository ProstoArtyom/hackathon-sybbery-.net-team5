using Newtonsoft.Json;

namespace TgBot.Utils
{
    public class LastActionStorage
    {
        public static Dictionary<long, LastAction> Storage { get; set; } = new();
        private static string _filename = "data.json";

        static LastActionStorage()
        {
            try
            {
                using StreamReader reader = new(_filename);
                var json = reader.ReadToEnd();
                var jObj = JsonConvert.DeserializeObject<Dictionary<long, LastAction>>(json);
                if(jObj == null)
                {
                    Console.WriteLine($"Failed to deserialize {_filename}");
                    return;
                }

                Storage = jObj;
            } 
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to load storage from {_filename}: ${ex.Message}");
            }
        }

        public static async Task SaveToFile()
        {
            var json = JsonConvert.SerializeObject(Storage);
            await File.WriteAllTextAsync(_filename, json);
        }
    }
}