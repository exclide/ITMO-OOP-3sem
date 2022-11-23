namespace Banks.Entities;

public class TimeManager
{
    private DateTime _currentTime;

    public TimeManager()
    {
        _currentTime = DateTime.Now;
    }

    public void PlusDay()
    {
        _currentTime = _currentTime.AddDays(1);
    }

    public void PlusWeek()
    {
        _currentTime = _currentTime.AddDays(7);
    }

    public void PlusMonth()
    {
        _currentTime = _currentTime.AddMonths(1);
    }

    public void PlusYear()
    {
        _currentTime = _currentTime.AddYears(1);
    }

    public void UpdateCurrent()
    {
        _currentTime = DateTime.Now;
    }
}