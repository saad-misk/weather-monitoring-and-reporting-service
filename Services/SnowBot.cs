using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Services
{
    public class SnowBot : IWeatherBot
    {
        public bool Enabled { get; set; }
        public double? TemperatureThreshold { get; set; }
        public string? Message { get; set; }

        public SnowBot(BotConfig botConfig)
        {
            Enabled = botConfig.Enabled;
            TemperatureThreshold = botConfig.TemperatureThreshold;
            Message = botConfig.Message;
        }
        public void Update(WeatherData data)
        {
            if (Enabled && data.Temperature < TemperatureThreshold)
            {
                ActivateBot();
            }
        }

        private void ActivateBot()
        {
            Console.WriteLine("SnowBot activated!");
            Console.WriteLine(Message);
        }
    }
}
