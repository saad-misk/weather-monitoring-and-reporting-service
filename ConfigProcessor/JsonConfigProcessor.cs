using Newtonsoft.Json;
using weatherMonitoringAndReportingService.AppSettings;
using weatherMonitoringAndReportingService.Bots;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.ConfigProcessor
{
    public class JsonConfigProcessor : IConfigProcessor
    {
        private readonly string _configFilePath = AppSettingsInitializer.AppSettingsInstance().ConfigFilePath;
        public void Add(BotType botType, BotConfig weatherData)
        { 
            var currentConfig = ReadFile();

            currentConfig.Add(botType, weatherData);

            var serializedConfig = Serialize(currentConfig);

            WriteFile(serializedConfig);
        }
        public void Remove(BotType botType)
        {
            var currentData = ReadFile();

            currentData.Remove(botType);

            var serializedConfig = Serialize(currentData);

            WriteFile(serializedConfig);
        }
        public void Update(BotType botType, BotConfig data)
        {
            var currentData = ReadFile();

            currentData[botType] = data;

            var serializedConfig = Serialize(currentData);

            WriteFile(serializedConfig);
        }
        public Dictionary<BotType, BotConfig> ReadFile()
        {
            string configJson = File.ReadAllText(_configFilePath);
            var botSettings = JsonConvert.DeserializeObject<Dictionary<BotType, BotConfig>>(configJson)
                    ?? throw new InvalidOperationException("Failed to parse bot configuration.");
            return botSettings!;
        }
        private string Serialize(Dictionary<BotType, BotConfig> data)
        {
            return JsonConvert.SerializeObject(data);
        }
        private void WriteFile(string data)
        {
            File.WriteAllText(_configFilePath, data);
        }
    }
}
