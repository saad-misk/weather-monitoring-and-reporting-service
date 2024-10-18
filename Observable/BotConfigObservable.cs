using weatherMonitoringAndReportingService.Models;
using weatherMonitoringAndReportingService.Observable;

public class BotConfigObservable : IObservable<WeatherData>
{
    private List<IObserver<WeatherData>> _observers;

    public BotConfigObservable()
    {
        _observers = new List<IObserver<WeatherData>>();
    }

    public IDisposable Subscribe(IObserver<WeatherData> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
        return new Unsubscriber<WeatherData>(_observers, observer);
    }

    public void NotifyObservers(WeatherData newConfig)
    {
        foreach (var observer in _observers)
        {
            try
            {
                observer.OnNext(newConfig);
            }
            catch (Exception ex)
            {
                observer.OnError(ex);
            }
        }
    }

    public void Complete()
    {
        foreach (var observer in _observers)
        {
            observer.OnCompleted();
        }
        _observers.Clear(); 
    }
}
