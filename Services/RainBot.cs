using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.Services
{
    public class RainBot : IWeatherBot
    {
        public bool Enabled {  get; set; }
        public double? HumidityThreshold {  get; set; }
        public string? Message {  get; set; }

        public RainBot(BotConfig botConfig)
        {
            Enabled = botConfig.Enabled;
            HumidityThreshold = botConfig.HumidityThreshold;
            Message = botConfig.Message;
        }
        public void Update(WeatherData data)
        {
            if(Enabled && data.Humidity >= HumidityThreshold)
            {
                ActivateBot();
            }
        }

        private void ActivateBot()
        {
            Console.WriteLine("Rain Bot is activated!");
            Console.WriteLine(Message);
        }

    }
}
