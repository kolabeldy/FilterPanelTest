using FilterPanelTest.Filter;

namespace FilterPanelTest;
public class MainWindowModel : BaseViewModel
{
    public FilterPanel FilterPanel { get; set; }

    protected FilterPanelViewModel filterPanelViewModel;

    protected bool _IsFilterPopupOpen = false;
    public bool IsFilterPopupOpen
    {
        get => _IsFilterPopupOpen;
        set
        {
            if (isClosePress)
                Set(ref _IsFilterPopupOpen, value);
            else Set(ref _IsFilterPopupOpen, true);
        }
    }

    protected bool isClosePress = false;
    protected void FilterClose()
    {
        isClosePress = true;
        IsFilterPopupOpen = false;
        isClosePress = false;
    }
    protected FilterSet filterSet;

    private List<FilterTable> GetFilters(FilterSet filter)
    {
        List<FilterTable> result = new();
        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodMin", Value = filterSet.StartPeriod });
        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodMax", Value = filterSet.EndPeriod });
        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodDynamicMin", Value = filterSet.StartDynamicPeriod });
        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodDynamicMax", Value = filterSet.EndDynamicPeriod });
        foreach (var r in filterSet.SelectedCC)
        {
            result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "CC", Value = r.Id });
        }
        foreach (var r in filterSet.SelectedER)
        {
            result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "ER", Value = r.Id });
        }

        return result;
    }
    protected void Refresh(FilterSet filterSet)
    {
        this.filterSet = filterSet;
        FilterTable filterTable = new();
        filterTable.Delete("Analysis", "Use");
        filterTable.AddRange(GetFilters(filterSet));
    }


    public MainWindowModel(bool periodVisible, bool ccVisible, bool erVisible, bool ntVisible)
    {
        filterSet = new();
        filterPanelViewModel = new FilterPanelViewModel(ref filterSet, periodVisible, ccVisible, erVisible, ntVisible);
        filterPanelViewModel.OnFilterPanelClosed += FilterClose;
        filterPanelViewModel.OnFilterChanged += Refresh;
        FilterPanel = new FilterPanel(filterPanelViewModel);
        Refresh(filterSet);
    }
}