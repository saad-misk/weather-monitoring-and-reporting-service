using weatherMonitoringAndReportingService.AppSettings;
using weatherMonitoringAndReportingService.Bots;
using weatherMonitoringAndReportingService.ConfigProcessor;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Config
{
    public class BotsConfigService
    {
        private readonly IConfigProcessor _configProcessor;
        private readonly Dictionary<BotType, BotConfig> _botsConfigurations;
        private readonly string _configFilePath = AppSettingsInitializer.AppSettingsInstance().ConfigFilePath;

        public BotsConfigService(IConfigProcessor configProcessor)
        {
            _configProcessor = configProcessor;
            _botsConfigurations = _configProcessor.ReadFile();
        }

        public void AddBotConfiguration(BotType botType, BotConfig configuration)
        {
            _botsConfigurations.Add(botType, configuration);
            _configProcessor.Add(botType, configuration);
        }

        public void RemoveBotConfiguration(BotType botType)
        {
            _botsConfigurations.Remove(botType);
            _configProcessor.Remove(botType);
        }

        public void UpdateBotConfiguration(BotType botType, BotConfig configuration)
        {
            _botsConfigurations[botType] = configuration;
            _configProcessor.Update(botType, configuration);
        }

        public BotConfig GetBotConfiguration(BotType type) =>  _botsConfigurations[type];
    }
}
