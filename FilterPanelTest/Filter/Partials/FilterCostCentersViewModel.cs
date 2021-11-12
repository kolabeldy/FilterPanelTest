namespace FilterPanelTest.Filter.Partials;
public class FilterCostCentersViewModel : INotifyPropertyChanged
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
            if (value > 0)
            {
                OtherComboBoxSelectedIndex = -1;
                SlaveComboBoxSelectedIndex = -1;
            }
            SelectedTechMainCCList = GetCurrentSelectList(SourceTechMainCCList, MainComboBoxSelectedIndex);
            SelectedCCList = GetCommonSelectList();
            IsChanged = true;
        }
    }
    private int _OtherComboBoxSelectedIndex = 0;
    public int OtherComboBoxSelectedIndex
    {
        get => _OtherComboBoxSelectedIndex;
        set
        {
            Set(ref _OtherComboBoxSelectedIndex, value);
            if (value > 0)
            {
                MainComboBoxSelectedIndex = -1;
                SlaveComboBoxSelectedIndex = -1;
            }
            SelectedTechOtherCCList = GetCurrentSelectList(SourceTechOtherCCList, OtherComboBoxSelectedIndex);
            SelectedCCList = GetCommonSelectList();
            IsChanged = true;
        }
    }
    private int _SlaveComboBoxSelectedIndex = 0;
    public int SlaveComboBoxSelectedIndex
    {
        get => _SlaveComboBoxSelectedIndex;
        set
        {
            Set(ref _SlaveComboBoxSelectedIndex, value);
            if (value > 0)
            {
                MainComboBoxSelectedIndex = -1;
                OtherComboBoxSelectedIndex = -1;
            }
            SelectedSlaveCCList = GetCurrentSelectList(SourceSlaveCCList, SlaveComboBoxSelectedIndex);
            SelectedCCList = GetCommonSelectList();
            IsChanged = true;
        }
    }
    public List<CostCenter> SourceTechMainCCList { get; set; } = new List<CostCenter>();
    public List<CostCenter> SourceTechOtherCCList { get; set; } = new List<CostCenter>();
    public List<CostCenter> SourceSlaveCCList { get; set; } = new List<CostCenter>();

    public List<CostCenter> SelectedTechMainCCList { get; set; }
    public List<CostCenter> SelectedTechOtherCCList { get; set; }
    public List<CostCenter> SelectedSlaveCCList { get; set; }
    public List<CostCenter> SelectedCCList { get; set; }


    public void ComboBoxClear(ComboBox par)
    {
        par.SelectedIndex = -1;
    }

    private CostCenter CostCenter { get; set; } = new();
    public FilterCostCentersViewModel()
    {
        SourceTechMainCCList = CostCenter.TechMainList;
        SourceTechOtherCCList = CostCenter.TechOtherList;
        SourceSlaveCCList = CostCenter.SlaveList;
        MainComboBoxSelectedIndex = 0;
        OtherComboBoxSelectedIndex = 0;
        SlaveComboBoxSelectedIndex = 0;
        CostCenter.SelectedList = SelectedCCList;
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
    private static List<CostCenter> GetCurrentSelectList(List<CostCenter> sourceList, int index)
    {
        List<CostCenter> result = new List<CostCenter>();
        if (index == 0)
        {
            int i = 0;
            foreach (CostCenter r in sourceList)
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
    private List<CostCenter> GetCommonSelectList()
    {
        List<CostCenter> result = new List<CostCenter>();
        if (SelectedTechMainCCList != null)
            result.AddRange(SelectedTechMainCCList);
        if (SelectedTechOtherCCList != null)
            result.AddRange(SelectedTechOtherCCList);
        if (SelectedSlaveCCList != null)
            result.AddRange(SelectedSlaveCCList);
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
