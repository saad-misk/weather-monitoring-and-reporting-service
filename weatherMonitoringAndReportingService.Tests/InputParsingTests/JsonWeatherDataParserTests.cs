using System;
using Newtonsoft.Json;
using weatherMonitoringAndReportingService.InputParsing;
using weatherMonitoringAndReportingService.Models;
using Xunit;

namespace weatherMonitoringAndReportingService.weatherMonitoringAndReportingService.Tests.InputParsingTests
{
    public class JsonWeatherDataParserTests
    {
        private readonly JsonWeatherDataParser _parser;

        public JsonWeatherDataParserTests()
        {
            _parser = new JsonWeatherDataParser();
        }

        [Fact]
        public void Parse_ValidJson_ReturnsWeatherData()
        {
            // Arrange
            var jsonInput = "{\"Humidity\": 75, \"Temperature\": 22}";
            var expectedWeatherData = new WeatherData
            {
                Humidity = 75,
                Temperature = 22
            };

            // Act
            var result = _parser.Parse(jsonInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedWeatherData.Humidity, result.Humidity);
            Assert.Equal(expectedWeatherData.Temperature, result.Temperature);
        }

        [Fact]
        public void Parse_NullInput_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _parser.Parse(null));
            Assert.Equal("Input cannot be null.", exception.ParamName);
        }

        [Fact]
        public void Parse_InvalidJson_ThrowsInvalidOperationException()
        {
            // Arrange
            var invalidJsonInput = "{InvalidJson}";

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _parser.Parse(invalidJsonInput));
            Assert.Equal("Deserialization failed; result is null.", exception.Message);
        }
    }
}
