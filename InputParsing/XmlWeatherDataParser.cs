
using System.Xml.Serialization;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.InputParsing
{
    public class XmlWeatherDataParser
    {
        WeatherState Parse(string input)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(WeatherState));

            using (StringReader reader = new StringReader(input))
            {   
                return (WeatherState)serializer.Deserialize(reader);
            }      
        }
    }

}
