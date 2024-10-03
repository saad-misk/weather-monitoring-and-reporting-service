using System.Xml;
using System.Xml.Serialization;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public class XmlWeatherDataParser : IWeatherDataParser
    {
        public WeatherData? Parse(string input)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(WeatherData));

                using (StringReader reader = new StringReader(input))
                {
                    return (WeatherData)serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Deserialization failed: {ex.Message}");
            }
            catch (XmlException ex)
            {
                Console.WriteLine($"Malformed XML: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }
    }

}
