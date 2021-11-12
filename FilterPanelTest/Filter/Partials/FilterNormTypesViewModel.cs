namespace FilterPanelTest.Filter.Partials;
public class FilterNormTypesViewModel : INotifyPropertyChanged
{
    private bool _IsChanged;
    public bool IsChanged
    {
        get => _IsChanged;
        set
        {
            Set(ref _IsChanged, value);
        }
    }

    private int _MainComboBoxSelectedIndex = 0;
    public int MainComboBoxSelectedIndex
    {
        get => _MainComboBoxSelectedIndex;
        set
        {
            Set(ref _MainComboBoxSelectedIndex, value);
            IsChanged = true;
        }
    }

    public FilterNormTypesViewModel()
    {

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
