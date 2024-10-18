namespace weatherMonitoringAndReportingService.Observable
{
    public sealed class Unsubscriber<WeatherData> : IDisposable
    {
        private readonly List<IObserver<WeatherData>> _observers;
        private readonly IObserver<WeatherData> _observer;

        public Unsubscriber(
            List<IObserver<WeatherData>> observers,
            IObserver<WeatherData> observer) => (_observers, _observer) = (observers, observer);

        public void Dispose() => _observers.Remove(_observer);
    }
}
