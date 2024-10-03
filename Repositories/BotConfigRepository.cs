using Newtonsoft.Json;
using weatherMonitoringAndReportingService.Models;
using weatherMonitoringAndReportingService.Services;

namespace weatherMonitoringAndReportingService.Repositories
{
    public class BotConfigRepository
    {
        private static readonly BotConfigRepository _instance = new BotConfigRepository();
        public string ConfigFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}../../../Config/BotsConfig.json";

        public static BotConfigRepository Instance { get { return _instance; } }

        private Dictionary<BotType, BotConfig> _bots;

        public Dictionary<BotType, BotConfig> BotsConfig { get { return _bots; } }

        private BotConfigRepository() 
        {
            LoadConfigFromFile(ConfigFilePath);
        }

        private void LoadConfigFromFile(string configFilePath)
        {
            try
            {
                if (!File.Exists(configFilePath))
                {
                    throw new FileNotFoundException("Bot configuration file not found.");
                }

                string configJson = File.ReadAllText(configFilePath);
                _bots = JsonConvert.DeserializeObject<Dictionary<BotType, BotConfig>>(configJson)
                        ?? throw new InvalidOperationException("Failed to parse bot configuration.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading bot configuration: {ex.Message}");
                _bots = new Dictionary<BotType, BotConfig>();
            }
        }
     }
}
