using Moq;
using weatherMonitoringAndReportingService.Bots;
using weatherMonitoringAndReportingService.Config;
using weatherMonitoringAndReportingService.ConfigProcessor;
using weatherMonitoringAndReportingService.Models;
//using weatherMonitoringAndReportingService.weatherMonitoringAndReportingService.Bots;
using Xunit;

namespace weatherMonitoringAndReportingService.weatherMonitoringAndReportingService.Tests.BotsTests
{
    public class RainBotTests
    {
        private readonly Mock<IConfigProcessor> _mockConfigProcessor;
        private readonly BotsConfigService _botsConfigService;
        private readonly RainBot _rainBot;

        public RainBotTests()
        {
            _mockConfigProcessor = new Mock<IConfigProcessor>();
            var botConfigurations = new Dictionary<BotType, BotConfig>();
            _botsConfigService = new BotsConfigService(_mockConfigProcessor.Object);
            _rainBot = new RainBot(_botsConfigService);
        }

        [Fact]
        public void OnCompleted_PrintsStatusUpdatesCompleted()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _rainBot.OnCompleted();

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("RainBot: Status updates completed.", output);
        }

        [Fact]
        public void OnError_PrintsErrorMessage()
        {
            // Arrange
            var exception = new Exception("Test error");
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _rainBot.OnError(exception);

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("RainBot: Error occurred - Test error", output);
        }

        [Fact]
        public void OnNext_ValidHumidity_ActivatesBot()
        {
            // Arrange
            var weatherData = new WeatherData { Humidity = 70 };
            var rainConfig = new BotConfig { HumidityThreshold = 60, Enabled = false, Message = "It's raining!" };

            _botsConfigService.AddBotConfiguration(BotType.RainBot, rainConfig);

            // Act
            _rainBot.OnNext(weatherData);

            // Assert
            var updatedConfig = _botsConfigService.GetBotConfiguration(BotType.RainBot);
            Assert.True(updatedConfig.Enabled);
            //Assert.Contains("RainBot: Activated!", StringWriter.ToString());
            //Assert.Contains("RainBot: It's raining!", stringWriter.ToString());
        }

        [Fact]
        public void OnNext_InvalidHumidity_DoesNotActivateBot()
        {
            // Arrange
            var weatherData = new WeatherData { Humidity = 50 };
            var rainConfig = new BotConfig { HumidityThreshold = 60, Enabled = true };

            _botsConfigService.AddBotConfiguration(BotType.RainBot, rainConfig);

            // Act
            _rainBot.OnNext(weatherData);

            // Assert
            var updatedConfig = _botsConfigService.GetBotConfiguration(BotType.RainBot);
            Assert.False(updatedConfig.Enabled);
        }
    }
}
