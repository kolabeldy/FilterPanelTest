namespace FilterPanelTest.FilterTree;
public class FilterPanelViewModel : BaseViewModel
{

    private bool _FiltersIsChanged = false;
    public bool FiltersIsChanged
    {
        get => _FiltersIsChanged;
        set
        {
            if (value)
                RefreshVisible = Visibility.Visible;
            else
                RefreshVisible = Visibility.Hidden;
            Set(ref _FiltersIsChanged, value);
        }
    }

    private Visibility _RefreshVisible = Visibility.Hidden;
    public Visibility RefreshVisible
    {
        get => _RefreshVisible;
        set
        {
            Set(ref _RefreshVisible, value);
        }
    }

    private FilterSet _FilterSet;
    public FilterSet FilterSet
    {
        get => _FilterSet;
        set
        {
            Set(ref _FilterSet, value);
        }
    }

    #region Для доработки

    //private List<FilterTable> GetFilters(FilterSet filter)
    //{
    //    List<FilterTable> result = new();
    //    result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodMin", Value = FilterSet.StartPeriod });
    //    result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodMax", Value = FilterSet.EndPeriod });
    //    result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodDynamicMin", Value = FilterSet.StartDynamicPeriod });
    //    result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodDynamicMax", Value = FilterSet.EndDynamicPeriod });
    //    foreach (var r in FilterSet.SelectedCC)
    //    {
    //        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "CC", Value = r.Id });
    //    }
    //    foreach (var r in FilterSet.SelectedER)
    //    {
    //        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "ER", Value = r.Id });
    //    }

    //    return result;
    //}
    //protected void Refresh(FilterSet filterSet)
    //{
    //    this.FilterSet = filterSet;
    //    FilterTable filterTable = new();
    //    filterTable.Delete("Analysis", "Use");
    //    filterTable.AddRange(GetFilters(filterSet));
    //}

    #endregion

    public FilterSection FilterDate { get; set; }
    public FilterSection FilterCC { get; set; }
    public FilterSection FilterER { get; set; }

    private List<FilterSection> _FilterSections = new();
    private List<FilterSectionViewModel> _FilterSectionViewModels = new();
    public FilterPanelViewModel(List<TreeFilterCollection> treeFilterCollections)
    {
        int i = 0;
        foreach(var r in treeFilterCollections)
        {
            _FilterSectionViewModels.Add(new FilterSectionViewModel());
            _FilterSectionViewModels[i].onChange += FilterOnChangeHandler;
            _FilterSectionViewModels[i].Init(treeFilterCollections[i].Title, treeFilterCollections[i].FilterCollection, treeFilterCollections[i].InitType);
            _FilterSections.Add(new FilterSection(_FilterSectionViewModels[i]));
            i++;
        }
        FilterDate = _FilterSections[0];
        FilterCC = _FilterSections[1];
        FilterER = _FilterSections[2];
    }

    protected RelayCommand _Refresh_Command;
    public RelayCommand Refresh_Command
    {
        get
        {
            return _Refresh_Command ??
                (_Refresh_Command = new RelayCommand(obj =>
                {
                    FiltersIsChanged = false;
                }));
        }
    }

    private void FilterOnChangeHandler()
    {
        FiltersIsChanged = true;
    }

}
