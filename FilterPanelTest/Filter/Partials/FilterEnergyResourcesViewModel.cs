namespace FilterPanelTest.Filter.Partials;
public class FilterEnergyResourcesViewModel : INotifyPropertyChanged
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

    private int _PrimeComboBoxSelectedIndex = 0;
    public int PrimeComboBoxSelectedIndex
    {
        get => _PrimeComboBoxSelectedIndex;
        set
        {
            Set(ref _PrimeComboBoxSelectedIndex, value);
            if (value > 0)
            {
                SecondaryComboBoxSelectedIndex = -1;
            }
            SelectedPrimeList = GetCurrentSelectList(SourcePrimeList, PrimeComboBoxSelectedIndex);
            SelectedERList = GetCommonSelectList();
            IsChanged = true;
        }
    }
    private int _SecondaryComboBoxSelectedIndex = 0;
    public int SecondaryComboBoxSelectedIndex
    {
        get => _SecondaryComboBoxSelectedIndex;
        set
        {
            Set(ref _SecondaryComboBoxSelectedIndex, value);
            if (value > 0)
            {
                PrimeComboBoxSelectedIndex = -1;
            }
            SelectedSecondaryList = GetCurrentSelectList(SourceSecondaryList, SecondaryComboBoxSelectedIndex);
            SelectedERList = GetCommonSelectList();
            IsChanged = true;
        }
    }
    public List<EnergyResource> SourcePrimeList { get; set; } = new List<EnergyResource>();
    public List<EnergyResource> SourceSecondaryList { get; set; } = new List<EnergyResource>();

    public List<EnergyResource> SelectedPrimeList { get; set; }
    public List<EnergyResource> SelectedSecondaryList { get; set; }
    public List<EnergyResource> SelectedERList { get; set; }


    public void ComboBoxClear(ComboBox par)
    {
        par.SelectedIndex = -1;
    }

    private CostCenter CostCenter { get; set; } = new();
    public FilterEnergyResourcesViewModel()
    {
        SourcePrimeList = EnergyResource.PrimeList;
        SourceSecondaryList = EnergyResource.SecondaryList;
        PrimeComboBoxSelectedIndex = 0;
        SecondaryComboBoxSelectedIndex = 0;
        EnergyResource.SelectedList = SelectedERList;
    }

    private RelayCommand _ComboBoxClear_Command;
    public RelayCommand ComboBoxClear_Command
    {
        get
        {
            return _ComboBoxClear_Command ??
                (_ComboBoxClear_Command = new RelayCommand(obj =>
                {
                    ComboBoxClear(obj as ComboBox);
                }));
        }
    }
    private static List<EnergyResource> GetCurrentSelectList(List<EnergyResource> sourceList, int index)
    {
        List<EnergyResource> result = new List<EnergyResource>();
        if (index == 0)
        {
            int i = 0;
            foreach (EnergyResource r in sourceList)
            {
                if (i > 0)
                    result.Add(r);
                i++;
            }
            return result;
        }
        if (index > 0)
        {
            result.Add(sourceList[index]);
            return result;
        }
        else
            return null;
    }
    private List<EnergyResource> GetCommonSelectList()
    {
        List<EnergyResource> result = new List<EnergyResource>();
        if (SelectedPrimeList != null)
            result.AddRange(SelectedPrimeList);
        if (SelectedSecondaryList != null)
            result.AddRange(SelectedSecondaryList);
        return result;
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
