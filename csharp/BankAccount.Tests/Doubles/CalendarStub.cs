namespace BankAccount.Test;

public class CalendarStub : ICalendar
{
    private DateTime? _on;

    public void ReturnOnce(DateTime on)
    {
        _on = on;
    }

    public DateTime CurrentDate
    {
        get
        {
            var result = _on;
            _on = null;
            return result.Value;
        }
    }
}