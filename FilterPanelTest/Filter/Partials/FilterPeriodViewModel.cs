namespace FilterPanelTest.Filter.Partials;
public class FilterPeriodViewModel : INotifyPropertyChanged
{
    public Period PeriodData { get; set; }

    private bool _IsChanged;
    public bool IsChanged
    {
        get => _IsChanged;
        set
        {
            Set(ref _IsChanged, value);
        }
    }

    private DateTime _DisplayDateStart;
    public DateTime DisplayDateStart
    {
        get => _DisplayDateStart;
        set
        {
            if (Set(ref _DisplayDateStart, value))
            {
                IsChanged = true;
            }
        }
    }
    private DateTime _DisplayDateEnd;
    public DateTime DisplayDateEnd
    {
        get => _DisplayDateEnd;
        set
        {
            if (Set(ref _DisplayDateEnd, value))
            {
                IsChanged = true;
            }
        }
    }

    private DateTime _SelectedStartDate;
    public DateTime SelectedStartDate
    {
        get => _SelectedStartDate;
        set
        {
            if (Set(ref _SelectedStartDate, value))
            {
                IsChanged = true;
                PeriodData.SelectedStartDate = value;
            }
        }
    }
    private DateTime _SelectedEndDate;
    public DateTime SelectedEndDate
    {
        get => _SelectedEndDate;
        set
        {
            if (Set(ref _SelectedEndDate, value))
            {
                IsChanged = true;
                PeriodData.SelectedEndDate = value;
            }
        }
    }

    public FilterPeriodViewModel()
    {
        PeriodData = new Period();
        DateTime currDate = new DateTime(Period.MaxYear, Period.MaxMonth, 1);
        DisplayDateStart = new DateTime(Period.MinYear, Period.MinMonth, 1);
        DisplayDateEnd = currDate;
        SelectedStartDate = currDate;
        SelectedEndDate = new DateTime(currDate.Year, currDate.Month, DateTime.DaysInMonth(currDate.Year, currDate.Month));
    }

    #region INotifyProperty

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    protected bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(PropertyName);
        return true;
    }


    #endregion

}
