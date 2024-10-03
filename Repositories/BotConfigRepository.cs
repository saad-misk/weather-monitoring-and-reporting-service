using Newtonsoft.Json;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Repositories
{
    public class BotConfigRepository
    {
        private static readonly BotConfigRepository _instance = new BotConfigRepository();

        private const string _configFilePath = @"Config/BotsConfig.json";

        public static BotConfigRepository Instance { get { return _instance; } }

        private List<BotConfig> _bots;

        public List<BotConfig> BotsConfig { get { return _bots; } }

        private BotConfigRepository() 
        {
            LoadConfigFromFile(_configFilePath);
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
                _bots = JsonConvert.DeserializeObject<List<BotConfig>>(configJson);

                if (_bots == null)
                {
                    throw new InvalidOperationException("Failed to parse bot configuration.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading bot configuration: {ex.Message}");
                _bots = new List<BotConfig>();
            }
        }
    }
}
