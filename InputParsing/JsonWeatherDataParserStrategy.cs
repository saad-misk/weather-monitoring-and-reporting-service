using weatherMonitoringAndReportingService.InputParsing;
using weatherMonitoringAndReportingService.Models;

public class JsonWeatherDataParserStrategy : IWeatherDataParserStrategy
{
    public WeatherData Parse(string input)
    {
        return  new JsonWeatherDataParser().Parse(input);
    }
}
