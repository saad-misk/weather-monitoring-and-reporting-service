using weatherMonitoringAndReportingService.Models;
using weatherMonitoringAndReportingService.Repositories;

namespace weatherMonitoringAndReportingService.Services
{
    public class WeatherMonitoringSystem
    {
        private Dictionary<BotType, BotConfig> _botsConfigs = BotConfigRepository.Instance.BotsConfig;

        private List<IWeatherBot> _bots { get => GetBotsList(); }
        public void ProccesWeatherData(WeatherData weatherData)
        {
            foreach(var bot in _bots)
            {
                bot.Update(weatherData);
            }
        }

        private List<IWeatherBot> GetBotsList()
        {
            var bots = new List<IWeatherBot>();
            foreach (var config in _botsConfigs)
            {
                switch (config.Key)
                {
                    case BotType.RainBot:
                        bots.Add(new RainBot(config.Value));
                        break;
                    case BotType.SunBot:
                        bots.Add(new SunBot(config.Value));
                        break;
                    case BotType.SnowBot:
                        bots.Add(new SnowBot(config.Value));
                        break;
                    default:
                        Console.WriteLine($"Unknown bot: {config.Key}");
                        break;
                }
            }
            return bots;
        }

    }
}
