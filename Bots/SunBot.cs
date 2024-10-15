using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Bots
{
    public class SunBot : IObserver<WeatherData>
    {
        private readonly BotsConfigService _botsConfigService;
        private readonly BotType _botType = BotType.SunBot;

        public SunBot(BotsConfigService configService)
        {
            _botsConfigService = configService;
        }

        public void OnCompleted()
        {
            Console.WriteLine($"{_botType}: Status updates completed.");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"{_botType}: Error occurred - {error.Message}");
        }

        public void OnNext(WeatherData value)
        {
            Console.WriteLine($"{_botType}: New weather status received.");
            UpdateConfiguration(value);
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
