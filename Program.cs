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
            Console.WriteLine("Welcome to the Weather Monitoring System!");

            while (true)
            {
                Console.WriteLine("\nEnter Weather Data in JSON or XML format.");
                Console.WriteLine("To exit, type 'exit'.");

                string input = ReadInputFromUser();

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                }

                WeatherMonitoringSystem system = new WeatherMonitoringSystem();
                IWeatherDataParser parser;
                WeatherData weatherData;

                try
                {
                    if (input.TrimStart().StartsWith("{"))
                    {
                        parser = new JsonWeatherDataParser();
                        weatherData = parser.Parse(input);
                        Console.WriteLine("JSON data successfully parsed.");
                    }
                    else if (input.TrimStart().StartsWith("<"))
                    {
                        parser = new XmlWeatherDataParser();
                        weatherData = parser.Parse(input);
                        Console.WriteLine("XML data successfully parsed.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input format. Please provide data in JSON or XML format.");
                        continue;
                    }

                    system.ProccesWeatherData(weatherData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while parsing: {ex.Message}");
                    Console.WriteLine("Please try again with valid data.");
                }
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
