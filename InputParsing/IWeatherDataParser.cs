using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public interface IWeatherDataParser
    {
        WeatherState? Parse(string input);
    }
}
