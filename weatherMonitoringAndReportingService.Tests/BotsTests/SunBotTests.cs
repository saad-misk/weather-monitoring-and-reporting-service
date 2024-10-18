using Moq;
using weatherMonitoringAndReportingService.Bots;
using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.ConfigProcessor;
using weatherMonitoringAndReportingService.Models;
//using weatherMonitoringAndReportingService.weatherMonitoringAndReportingService.Bots;
using Xunit;

namespace weatherMonitoringAndReportingService.weatherMonitoringAndReportingService.Tests.BotsTests
{
    public class SunBotTests
    {
        private readonly Mock<IConfigProcessor> _mockConfigProcessor;
        private readonly BotsConfigService _botsConfigService;
        private readonly SunBot _sunBot;

        public SunBotTests()
        {
            _mockConfigProcessor = new Mock<IConfigProcessor>();
            var botConfigurations = new Dictionary<BotType, BotConfig>();
            _botsConfigService = new BotsConfigService(_mockConfigProcessor.Object);
            _sunBot = new SunBot(_botsConfigService);
        }

        [Fact]
        public void OnCompleted_PrintsStatusUpdatesCompleted()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _sunBot.OnCompleted();

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("SunBot: Status updates completed.", output);
        }

        [Fact]
        public void OnError_PrintsErrorMessage()
        {
            // Arrange
            var exception = new Exception("Test error");
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _sunBot.OnError(exception);

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("SunBot: Error occurred - Test error", output);
        }

        [Fact]
        public void OnNext_TemperatureAboveThreshold_ActivatesBot()
        {
            // Arrange
            var weatherData = new WeatherData { Temperature = 35 };
            var sunConfig = new BotConfig { TemperatureThreshold = 30, Enabled = false, Message = "It's sunny!" };

            _botsConfigService.AddBotConfiguration(BotType.SunBot, sunConfig);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _sunBot.OnNext(weatherData);

            // Assert
            var updatedConfig = _botsConfigService.GetBotConfiguration(BotType.SunBot);
            Assert.True(updatedConfig.Enabled);
            Assert.Contains("SunBot :  activated!", stringWriter.ToString());
            Assert.Contains("SunBot: It's sunny!", stringWriter.ToString());
        }

        [Fact]
        public void OnNext_TemperatureBelowThreshold_DeactivatesBot()
        {
            // Arrange
            var weatherData = new WeatherData { Temperature = 25 };
            var sunConfig = new BotConfig { TemperatureThreshold = 30, Enabled = true };

            _botsConfigService.AddBotConfiguration(BotType.SunBot, sunConfig);

            // Act
            _sunBot.OnNext(weatherData);

            // Assert
            var updatedConfig = _botsConfigService.GetBotConfiguration(BotType.SunBot);
            Assert.False(updatedConfig.Enabled);
        }
    }
}
