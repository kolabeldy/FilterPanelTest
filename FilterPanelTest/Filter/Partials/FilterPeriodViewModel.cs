namespace FilterPanelTest.Filter.Partials;
public class FilterPeriodViewModel : INotifyPropertyChanged
{
    public List<Period> SourcePeriodsList { get; set; }

    private Period _StartPeriodBoxSelectedItem;
    public Period StartPeriodBoxSelectedItem
    {
        get => _StartPeriodBoxSelectedItem;
        set
        {
            Set(ref _StartPeriodBoxSelectedItem, value);
            SelectedStartPeriod = value.Id;
            IsChanged = true;
        }
    }
    private Period _EndPeriodBoxSelectedItem;
    public Period EndPeriodBoxSelectedItem
    {
        get => _EndPeriodBoxSelectedItem;
        set
        {
            Set(ref _EndPeriodBoxSelectedItem, value);
            SelectedEndPeriod = value.Id;
            IsChanged = true;
        }
    }
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

    private int _SelectedStartPeriod;
    public int SelectedStartPeriod
    {
        get => _SelectedStartPeriod;
        set
        {
            if (Set(ref _SelectedStartPeriod, value))
            {
                PeriodData.SelectedStartPeriod = value;
                PeriodData.SetDynamicPeriods();
                IsChanged = true;
            }
        }
    }
    private int _SelectedEndPeriod;
    public int SelectedEndPeriod
    {
        get => _SelectedEndPeriod;
        set
        {
            if (Set(ref _SelectedEndPeriod, value))
            {
                PeriodData.SelectedEndPeriod = value;
                PeriodData.SetDynamicPeriods();
                IsChanged = true;
            }
        }
    }

    public FilterPeriodViewModel()
    {
        PeriodData = new Period();
        int currPeriod = Period.MaxPeriod;
        SelectedStartPeriod = currPeriod;
        SelectedEndPeriod = currPeriod;
        SourcePeriodsList = Period.Periods;
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
