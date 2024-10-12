using weatherMonitoringAndReportingService.Bots;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Services
{
    public interface IWeatherStationSubject
    {
        void Subscribe(IWeatherBot bot);
        void UnSubscribe(IWeatherBot bot);
        void Notify(WeatherData state);
    }
}
