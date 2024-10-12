using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Bots
{
    public class RainBot : IWeatherBot
    {
        private readonly BotsConfigService _botsConfigService;
        private readonly BotType _botType = BotType.RainBot;

        public RainBot(BotsConfigService configService)
        {
            _botsConfigService = configService;
        }
        public void UpdateConfiguration(WeatherData data)
        {
            var rainConfig = _botsConfigService.GetBotConfiguration(_botType);
            if (data.Humidity >= rainConfig.HumidityThreshold)
            {
                rainConfig.Enabled = true;
                Console.WriteLine($"{_botType} :  activated!");
                Console.WriteLine($"{_botType}: {rainConfig.Message}");
            }
            else
            {
                rainConfig.Enabled = false;
            }
            _botsConfigService.UpdateBotConfiguration(_botType, rainConfig);
        }
    }
}
