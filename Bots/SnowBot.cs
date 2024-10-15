using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Bots
{
    public class SnowBot : IObserver<WeatherData>
    {
        private readonly BotsConfigService _botsConfigService;
        private readonly BotType _botType = BotType.SnowBot;

        public SnowBot(BotsConfigService configService)
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
