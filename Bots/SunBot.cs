using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Bots
{
    public class SunBot : IWeatherBot
    {
        private readonly BotsConfigService _botsConfigService;
        private readonly BotType _botType = BotType.SunBot;

        public SunBot(BotsConfigService configService)
        {
            _botsConfigService = configService;
        }
        public void UpdateConfiguration(WeatherData data)
        {
            var sunConfig = _botsConfigService.GetBotConfiguration(_botType);
            if (data.Temperature > sunConfig.TemperatureThreshold)
            {
                sunConfig.Enabled = true;
                Console.WriteLine($"{_botType} :  activated!");
                Console.WriteLine($"{_botType}: {sunConfig.Message}");
            }
            else
            {
                sunConfig.Enabled = false;
            }
            _botsConfigService.UpdateBotConfiguration(_botType, sunConfig);
        }
    }
}
