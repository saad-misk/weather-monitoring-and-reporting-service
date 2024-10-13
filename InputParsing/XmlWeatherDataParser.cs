using System.Xml.Serialization;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public class XmlWeatherDataParser : IWeatherDataParser
    {
        public WeatherData? Parse(string input)
        {
            var serializer = new XmlSerializer(typeof(WeatherData));
            using var reader = new StringReader(input);
            return (WeatherData?)serializer.Deserialize(reader);
        }
    }

}
