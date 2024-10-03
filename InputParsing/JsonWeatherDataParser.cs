using Newtonsoft.Json;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public class JsonWeatherDataParser : IWeatherDataParser
    {
        public WeatherData? Parse(string input)
        {
            return JsonConvert.DeserializeObject<WeatherData>(input);
        }
    }
}
