using System;
using System.IO;
using System.Xml.Serialization;
using weatherMonitoringAndReportingService.InputParsing;
using weatherMonitoringAndReportingService.Models;
using Xunit;

namespace weatherMonitoringAndReportingService.weatherMonitoringAndReportingService.Tests.InputParsingTests
{
    public class XmlWeatherDataParserTests
    {
        private readonly XmlWeatherDataParser _parser;

        public XmlWeatherDataParserTests()
        {
            _parser = new XmlWeatherDataParser();
        }

        [Fact]
        public void Parse_ValidXml_ReturnsWeatherData()
        {
            // Arrange
            var xmlInput = "<WeatherData><Humidity>75</Humidity><Temperature>22</Temperature></WeatherData>";
            var expectedWeatherData = new WeatherData
            {
                Humidity = 75,
                Temperature = 22
            };

            // Act
            var result = _parser.Parse(xmlInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedWeatherData.Humidity, result.Humidity);
            Assert.Equal(expectedWeatherData.Temperature, result.Temperature);
        }

        [Fact]
        public void Parse_InvalidXml_ThrowsInvalidOperationException()
        {
            // Arrange
            var invalidXmlInput = "<WeatherData><Humidity>75<Humidity></WeatherData>"; // Malformed XML

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _parser.Parse(invalidXmlInput));
        }

        [Fact]
        public void Parse_NullInput_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _parser.Parse(null));
            Assert.Equal("input", exception.ParamName);
        }
    }
}
