using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Bots
{
    public class RainBot : IObserver<WeatherData>
    {
        private readonly BotsConfigService _botsConfigService;
        private readonly BotType _botType = BotType.RainBot;

        public RainBot(BotsConfigService configService)
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
            var rainConfig = _botsConfigService.GetBotConfiguration(_botType);
            if (data.Humidity >= rainConfig.HumidityThreshold)
            {
                rainConfig.Enabled = true;
                Console.WriteLine($"{_botType}: Activated!");
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
