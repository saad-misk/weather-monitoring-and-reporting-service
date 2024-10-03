using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public class JsonWeatherDataParser : IWeathreDataParser
    {
        WeatherState Parse(string input)
        {
            return JsonConvert.DeserializeObject<WeatherState>(input);
        }
    }
}
