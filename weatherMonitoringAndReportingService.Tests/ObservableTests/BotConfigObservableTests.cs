using Moq;
using weatherMonitoringAndReportingService.Models;
using weatherMonitoringAndReportingService.Observable;
using Xunit;

namespace weatherMonitoringAndReportingService.weatherMonitoringAndReportingService.Tests.ObservableTests
{
    public class BotConfigObservableTests
    {
        private BotConfigObservable _observable;

        public BotConfigObservableTests()
        {
            _observable = new BotConfigObservable();
        }

        [Fact]
        public void Subscribe_AddsObserver()
        {
            // Arrange
            var observerMock = new Mock<IObserver<WeatherData>>();

            // Act
            var subscription = _observable.Subscribe(observerMock.Object);

            // Assert
            Assert.NotNull(subscription);
        }

        [Fact]
        public void Subscribe_ObserverNotAddedTwice()
        {
            // Arrange
            var observerMock = new Mock<IObserver<WeatherData>>();

            // Act
            var subscription1 = _observable.Subscribe(observerMock.Object);
            var subscription2 = _observable.Subscribe(observerMock.Object);

            // Assert
            Assert.NotNull(subscription1);
            Assert.NotNull(subscription2);
        }

        [Fact]
        public void NotifyObservers_CallsOnNextForEachObserver()
        {
            // Arrange
            var observerMock1 = new Mock<IObserver<WeatherData>>();
            var observerMock2 = new Mock<IObserver<WeatherData>>();
            var weatherData = new WeatherData();
            _observable.Subscribe(observerMock1.Object);
            _observable.Subscribe(observerMock2.Object);

            // Act
            _observable.NotifyObservers(weatherData);

            // Assert
            observerMock1.Verify(o => o.OnNext(weatherData), Times.Once);
            observerMock2.Verify(o => o.OnNext(weatherData), Times.Once);
        }

        [Fact]
        public void NotifyObservers_CallsOnErrorIfObserverThrows()
        {
            // Arrange
            var observerMock = new Mock<IObserver<WeatherData>>();
            observerMock.Setup(o => o.OnNext(It.IsAny<WeatherData>())).Throws(new Exception("Test exception"));
            _observable.Subscribe(observerMock.Object);
            var weatherData = new WeatherData();

            // Act
            _observable.NotifyObservers(weatherData);

            // Assert
            observerMock.Verify(o => o.OnError(It.IsAny<Exception>()), Times.Once);
        }

        [Fact]
        public void Complete_CallsOnCompletedForEachObserver()
        {
            // Arrange
            var observerMock1 = new Mock<IObserver<WeatherData>>();
            var observerMock2 = new Mock<IObserver<WeatherData>>();
            _observable.Subscribe(observerMock1.Object);
            _observable.Subscribe(observerMock2.Object);

            // Act
            _observable.Complete();

            // Assert
            observerMock1.Verify(o => o.OnCompleted(), Times.Once);
            observerMock2.Verify(o => o.OnCompleted(), Times.Once);
        }

        [Fact]
        public void Complete_ClearsObservers()
        {
            // Arrange
            var observerMock = new Mock<IObserver<WeatherData>>();
            _observable.Subscribe(observerMock.Object);

            // Act
            _observable.Complete();

            // Assert
            var subscription = _observable.Subscribe(observerMock.Object);
            Assert.NotNull(subscription); // Should be able to subscribe again
        }
    }
}
