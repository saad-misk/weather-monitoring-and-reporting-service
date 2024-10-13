using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Bots
{
    public interface IWeatherBot
    {
        void UpdateConfiguration(WeatherData data);
    }
}
