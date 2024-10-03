using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Services
{
    public interface IWeatherBot
    {
        void Update(WeatherData data);
    }
}
