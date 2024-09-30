using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringAndReportingService.Models
{
    public class BotConfig
    {
        public bool IsEnabled {  get; set; }
        public double Threshold {  get; set; }
        public string? Message { get; set; }

    }
}
