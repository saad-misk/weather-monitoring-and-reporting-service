using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public class XmlWeatherDataParserStrategy : IWeatherDataParserStrategy
    {
        public WeatherData Parse(string input)
        {
            return new XmlWeatherDataParser().Parse(input);
        }
    }
}
