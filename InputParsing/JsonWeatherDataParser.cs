using Newtonsoft.Json;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public class JsonWeatherDataParser : IWeatherDataParserStrategy
    {
        public WeatherData Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("Input cannot be null.");
            }
            WeatherData? weatherData;
            try
            {
                weatherData = JsonConvert.DeserializeObject<WeatherData>(input);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Deserialization failed; result is null.");
            }
            
            return weatherData;
        }
    }
}
