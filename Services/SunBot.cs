using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Services
{
    public class SunBot : IWeatherBot
    {
        public bool Enabled { get; set; }
        public double TemperatureThreshold { get; set; }
        public string? Message { get; set; }

        public SunBot(BotConfig botConfig)
        {
            Enabled = botConfig.IsEnabled;
            TemperatureThreshold = botConfig.Threshold;
            Message = botConfig.Message;
        }
        public void Update(WeatherState data)
        {
            if (Enabled && data.Temperature > TemperatureThreshold)
            {
                ActivateBot();
            }
        }

        private void ActivateBot()
        {
            Console.WriteLine("SunBot activated!");
            Console.WriteLine(Message);
        }
    }
}
