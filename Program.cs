
using System.Text;
using weatherMonitoringAndReportingService.InputParsing;
using weatherMonitoringAndReportingService.Models;
using weatherMonitoringAndReportingService.Services;

namespace WeatherApp
{
    class Program
    {
        static void Main()
        {

            while (true){

                Console.WriteLine("Enter Weather Data(JSON or XML)");
                string input = ReadInputFromUser();

                WeatherMonitoringSystem system = new WeatherMonitoringSystem();
                IWeatherDataParser parser;
                WeatherData x;
                if (input.TrimStart().StartsWith("{"))
                {
                    parser = new JsonWeatherDataParser();
                     x = parser.Parse(input);
                }
                else
                {
                    parser = new XmlWeatherDataParser();
                    x = parser.Parse(input);
                }

                system.ProccesWeatherData(x);

            }

        }
        static string ReadInputFromUser()
        {
            Console.WriteLine("Please enter the weather data:");
            Console.WriteLine("Finish your input by pressing Enter on an empty line.");

            StringBuilder userInput = new StringBuilder();
            string? line;

            // Read until the user presses Enter on an empty line
            while ((line = Console.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
            {
                userInput.AppendLine(line);
            }

            return userInput.ToString();
        }
    }
}