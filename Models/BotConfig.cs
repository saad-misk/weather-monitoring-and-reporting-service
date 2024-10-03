using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringAndReportingService.Models
{
    public class BotConfig
    {
        public bool Enabled { get; set; }
        public double? HumidityThreshold { get; set; }  // Nullable, since not all bots use humidity
        public double? TemperatureThreshold { get; set; }  // Nullable, since not all bots use temperature
        public string Message { get; set; }
    }
}
