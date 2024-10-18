using System.Xml.Serialization;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public class XmlWeatherDataParser : IWeatherDataParserStrategy
    {
        public WeatherData? Parse(string input)
        {
            if( input == null)
            {
                throw new ArgumentNullException("input");
            }
            var serializer = new XmlSerializer(typeof(WeatherData));
            using var reader = new StringReader(input);
            WeatherData? weatherData;
            try
            {
                weatherData = serializer.Deserialize(reader) as WeatherData;
            }
            catch
            {
                throw new InvalidOperationException("Deserialization failed; result is null.");
            }
            return weatherData;
        }
    }

}
