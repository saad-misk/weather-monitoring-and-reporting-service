using System.Text;
using weatherMonitoringAndReportingService.Bots;
using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.ConfigProcessor;
using weatherMonitoringAndReportingService.InputParsing;
using weatherMonitoringAndReportingService.Models;

namespace WeatherApp
{
    class Program
    {
        private static BotConfigObservable _weatherStation = new();

        static void Main(string[] args)
        {
            InitializeApp();

            IWeatherDataParserStrategy parserStrategy = GetParserStrategy();

            string userInput = GetUserInput();

            WeatherData weatherDetails = parserStrategy.Parse(userInput)!;
            _weatherStation.NotifyObservers(weatherDetails);
        }

        public static void InitializeApp()
        {
            BotsConfigService weatherConfigurationService = new(new JsonConfigProcessor());
            _weatherStation.Subscribe(new RainBot(weatherConfigurationService));
            _weatherStation.Subscribe(new SnowBot(weatherConfigurationService));
            _weatherStation.Subscribe(new SunBot(weatherConfigurationService));
        }

        private static IWeatherDataParserStrategy GetParserStrategy()
        {
            Console.WriteLine("Please choose input format:\n1. JSON\n2. XML");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 2))
            {
                Console.WriteLine("Invalid choice! Please enter 1 or 2.");
            }

            return choice switch
            {
                1 => new JsonWeatherDataParser(),
                2 => new XmlWeatherDataParser(),
                _ => null!
            };
        }

        private static string GetUserInput()
        {
            Console.WriteLine("Enter weather status (type 'STOP' to finish):");

            StringBuilder userInput = new StringBuilder();
            string line;
            while ((line = Console.ReadLine()!) != "STOP")
            {
                userInput.AppendLine(line);
            }

            return userInput.ToString();
        }
    }
}
