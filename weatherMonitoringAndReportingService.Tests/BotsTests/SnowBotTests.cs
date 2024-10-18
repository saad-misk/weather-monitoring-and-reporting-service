using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using weatherMonitoringAndReportingService.Bots;
using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.ConfigProcessor;
using weatherMonitoringAndReportingService.Models;
//using weatherMonitoringAndReportingService.weatherMonitoringAndReportingService.Bots;
using Xunit;

namespace weatherMonitoringAndReportingService.weatherMonitoringAndReportingService.Tests.BotsTests
{
    public class SnowBotTests
    {
        private readonly Mock<IConfigProcessor> _mockConfigProcessor;
        private readonly BotsConfigService _botsConfigService;
        private readonly SnowBot _snowBot;

        public SnowBotTests()
        {
            _mockConfigProcessor = new Mock<IConfigProcessor>();
            var botConfigurations = new Dictionary<BotType, BotConfig>();
            _botsConfigService = new BotsConfigService(_mockConfigProcessor.Object);
            _snowBot = new SnowBot(_botsConfigService);
        }

        [Fact]
        public void OnCompleted_PrintsStatusUpdatesCompleted()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _snowBot.OnCompleted();

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("SnowBot: Status updates completed.", output);
        }

        [Fact]
        public void OnError_PrintsErrorMessage()
        {
            // Arrange
            var exception = new Exception("Test error");
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _snowBot.OnError(exception);

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("SnowBot: Error occurred - Test error", output);
        }

        [Fact]
        public void OnNext_TemperatureBelowThreshold_ActivatesBot()
        {
            // Arrange
            var weatherData = new WeatherData { Temperature = -5 };
            var snowConfig = new BotConfig { TemperatureThreshold = 0, Enabled = false, Message = "It's snowing!" };

            _botsConfigService.AddBotConfiguration(BotType.SnowBot, snowConfig);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _snowBot.OnNext(weatherData);

            // Assert
            var updatedConfig = _botsConfigService.GetBotConfiguration(BotType.SnowBot);
            Assert.True(updatedConfig.Enabled);
            Assert.Contains("SnowBot :  activated!", stringWriter.ToString());
            Assert.Contains("SnowBot: It's snowing!", stringWriter.ToString());
        }

        [Fact]
        public void OnNext_TemperatureAboveThreshold_DeactivatesBot()
        {
            // Arrange
            var weatherData = new WeatherData { Temperature = 5 };
            var snowConfig = new BotConfig { TemperatureThreshold = 0, Enabled = true };

            _botsConfigService.AddBotConfiguration(BotType.SnowBot, snowConfig);

            // Act
            _snowBot.OnNext(weatherData);

            // Assert
            var updatedConfig = _botsConfigService.GetBotConfiguration(BotType.SnowBot);
            Assert.False(updatedConfig.Enabled);
        }
    }
}
