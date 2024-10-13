using weatherMonitoringAndReportingService.Bots;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Services
{
    public class WeatherStation : IWeatherStationSubject
    {
        private List<IWeatherBot> _bots = [];
        public void Subscribe(IWeatherBot bot) => _bots.Add(bot);
        public void UnSubscribe(IWeatherBot bot) => _bots.Remove(bot);
        public void Notify(WeatherData weatherData)
        {
            foreach(var bot in _bots)
            {
                bot.UpdateConfiguration(weatherData);
            }
        }
    }
}
