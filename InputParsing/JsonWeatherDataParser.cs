using Newtonsoft.Json;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public class JsonWeatherDataParser : IWeatherDataParser
    {
        public WeatherData Parse(string input)
        {
            var weatherData = JsonConvert.DeserializeObject<WeatherData>(input);
            if (weatherData == null)
            {
                throw new InvalidOperationException("Deserialization failed; result is null.");
            }
            return weatherData;
        }
    }
}
