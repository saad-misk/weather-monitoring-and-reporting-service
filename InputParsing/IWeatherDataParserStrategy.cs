using weatherMonitoringAndReportingService.Models;

public interface IWeatherDataParserStrategy
{
    WeatherData Parse(string input);
}
