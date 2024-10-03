using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public interface IWeatherDataParser
    {
        WeatherData? Parse(string input);
    }
}
