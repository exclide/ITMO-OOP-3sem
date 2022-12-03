using Banks.Exceptions;

namespace Banks.Entities;

public class TimeMachine : IObservable<DateOnly>
{
    private readonly ICollection<IObserver<DateOnly>> _observers;
    private DateOnly _date;

    public TimeMachine(DateOnly date)
    {
        _date = date;
        _observers = new List<IObserver<DateOnly>>();
    }

    public DateOnly Date
    {
        get => _date;
        private set
        {
            if (value < _date)
            {
                throw new BankException("Can't time travel to past");
            }

            _date = value;
            foreach (var sub in _observers)
            {
                sub.OnNext(_date);
            }
        }
    }

    public void AddDays(int days)
    {
        Date = Date.AddDays(days);
    }

    public void AddMonths(int months)
    {
        Date = Date.AddMonths(months);
    }

    public void AddYears(int years)
    {
        Date = Date.AddYears(years);
    }

    public void UpdateCurrent()
    {
        Date = DateOnly.FromDateTime(DateTime.Now);
    }

    public IDisposable Subscribe(IObserver<DateOnly> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return new Unsubscriber(_observers, observer);
    }

    private class Unsubscriber : IDisposable
    {
        private ICollection<IObserver<DateOnly>> _observers;
        private IObserver<DateOnly> _observer;

        public Unsubscriber(ICollection<IObserver<DateOnly>> observers, IObserver<DateOnly> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer is not null)
            {
                _observers.Remove(_observer);
            }
        }
    }
}