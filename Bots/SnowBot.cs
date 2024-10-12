using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Bots
{
    public class SnowBot : IWeatherBot
    {
        private readonly BotsConfigService _botsConfigService;
        private readonly BotType _botType = BotType.SnowBot;

        public SnowBot(BotsConfigService configService)
        {
            _botsConfigService = configService;
        }
        public void UpdateConfiguration(WeatherData data)
        {
            var snowConfig = _botsConfigService.GetBotConfiguration(_botType);
            if (data.Temperature < snowConfig.TemperatureThreshold)
            {
                snowConfig.Enabled = true;
                Console.WriteLine($"{_botType} :  activated!");
                Console.WriteLine($"{_botType}: {snowConfig.Message}");
            }
            else
            {
                snowConfig.Enabled = false;
            }
            _botsConfigService.UpdateBotConfiguration(_botType, snowConfig);
        }
    }
}
