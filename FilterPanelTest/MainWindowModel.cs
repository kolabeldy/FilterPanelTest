using FilterPanelTest.Filter;

namespace FilterPanelTest;
public class MainWindowModel : BaseViewModel
{
    private List<TreePerson> filterDateList;

    public FilterSection FilterDate { get; set; }
    public FilterSection FilterCC { get; set; }
    public FilterSection FilterER { get; set; }


    private FilterSectionPeriodViewModel modelDate;
    private FilterSectionCostCentersViewModel modelCC;
    private FilterSectionEnergyResourcesViewModel modelER;

    public bool FiltersIsChanged;




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

    private FilterSet _FilterSet;
    public FilterSet FilterSet
    {
        get => _FilterSet;
        set
        {
            Set(ref _FilterSet, value);
            TitleStartPeriod = value.StartPeriod;
            TitleEndPeriod = value.EndPeriod;
            TitleStartDynamicPeriod = value.StartDynamicPeriod;
            TitleEndDynamicPeriod = value.EndDynamicPeriod;

            TitleSelectedCCGroup = value.TitleSelectedCCGroup;
            TitleSelectedERGroup = value.TitleSelectedERGroup;
        }
    }
    private int _TitleStartPeriod;
    public int TitleStartPeriod
    {
        get => _TitleStartPeriod;
        set
        {
            Set(ref _TitleStartPeriod, value);
        }
    }
    private int _TitleEndPeriod;
    public int TitleEndPeriod
    {
        get => _TitleEndPeriod;
        set
        {
            Set(ref _TitleEndPeriod, value);
        }
    }
    private int _TitleStartDynamicPeriod;
    public int TitleStartDynamicPeriod
    {
        get => _TitleStartDynamicPeriod;
        set
        {
            Set(ref _TitleStartDynamicPeriod, value);
        }
    }
    private int _TitleEndDynamicPeriod;
    public int TitleEndDynamicPeriod
    {
        get => _TitleEndDynamicPeriod;
        set
        {
            Set(ref _TitleEndDynamicPeriod, value);
        }
    }

    private string _TitleSelectedCCGroup;
    public string TitleSelectedCCGroup
    {
        get => _TitleSelectedCCGroup;
        set
        {
            Set(ref _TitleSelectedCCGroup, value);
        }
    }
    private string _TitleSelectedERGroup;
    public string TitleSelectedERGroup
    {
        get => _TitleSelectedERGroup;
        set
        {
            Set(ref _TitleSelectedERGroup, value);
        }
    }


    private List<FilterTable> GetFilters(FilterSet filter)
    {
        List<FilterTable> result = new();
        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodMin", Value = FilterSet.StartPeriod });
        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodMax", Value = FilterSet.EndPeriod });
        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodDynamicMin", Value = FilterSet.StartDynamicPeriod });
        result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "PeriodDynamicMax", Value = FilterSet.EndDynamicPeriod });
        foreach (var r in FilterSet.SelectedCC)
        {
            result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "CC", Value = r.Id });
        }
        foreach (var r in FilterSet.SelectedER)
        {
            result.Add(new FilterTable { Category = "Analysis", Item = "Use", Indicator = "ER", Value = r.Id });
        }

        return result;
    }
    protected void Refresh(FilterSet filterSet)
    {
        this.FilterSet = filterSet;
        FilterTable filterTable = new();
        filterTable.Delete("Analysis", "Use");
        filterTable.AddRange(GetFilters(filterSet));
    }


    public MainWindowModel(bool periodVisible, bool ccVisible, bool erVisible, bool ntVisible)
    {
        OldFilterInit(periodVisible, ccVisible, erVisible, ntVisible);
        NewFilterInit();
    }

    private void OldFilterInit(bool periodVisible, bool ccVisible, bool erVisible, bool ntVisible)
    {
        FilterSet filterSet = new();
        FilterSet = new();
        filterPanelViewModel = new FilterPanelViewModel(ref filterSet, periodVisible, ccVisible, erVisible, ntVisible);
        FilterSet = filterSet;
        filterPanelViewModel.OnFilterPanelClosed += FilterClose;
        filterPanelViewModel.OnFilterChanged += Refresh;
        FilterPanel = new FilterPanel(filterPanelViewModel);
        Refresh(FilterSet);
    }
    private void NewFilterInit()
    {
        modelDate = new FilterSectionPeriodViewModel();
        modelDate.onChange += FilterDateOnChangeHandler;
        modelDate.Init("Период:", TreeInitType.Last);
        FilterDate = new FilterSection(modelDate);

        modelCC = new FilterSectionCostCentersViewModel();
        modelCC.onChange += FilterDateOnChangeHandler;
        modelCC.Init("Центры затрат:", TreeInitType.All);
        FilterCC = new FilterSection(modelCC);

        modelER = new FilterSectionEnergyResourcesViewModel();
        modelER.onChange += FilterDateOnChangeHandler;
        modelER.Init("Энергоресурсы:", TreeInitType.All);
        FilterER = new FilterSection(modelER);
    }
    private void FilterDateOnChangeHandler()
    {
        FiltersIsChanged = true;
        filterDateList = modelDate.FilterList;
    }

}