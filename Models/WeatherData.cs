using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringAndReportingService.Models
{
    public class WeatherData
    {
        [Required]
        public string? Location { get; set; }

        [Required]
        public double Temperature {  get; set; }

        [Required]
        public double Humidity {  get; set; }

        public override string ToString()
        {
            return  $"Location: {Location}\n" +
                    $"Temperature: {Temperature}\n" +
                    $"Humidty: {Humidity}\n";
        }

    }
}
