namespace FilterPanelTest.FilterTree;
public class FilterPanelViewModel : BaseViewModel
{
    private List<TreePerson> filterDateList;
    private List<TreePerson> filterCCList;
    private List<TreePerson> filterERList;

    public FilterSection FilterDate { get; set; }
    public FilterSection FilterCC { get; set; }
    public FilterSection FilterER { get; set; }


    private FilterSectionViewModel modelDate;
    private FilterSectionViewModel modelCC;
    private FilterSectionViewModel modelER;

    public bool FiltersIsChanged;

    //protected bool _IsFilterPopupOpen = false;
    //public bool IsFilterPopupOpen
    //{
    //    get => _IsFilterPopupOpen;
    //    set
    //    {
    //        if (isClosePress)
    //            Set(ref _IsFilterPopupOpen, value);
    //        else Set(ref _IsFilterPopupOpen, true);
    //    }
    //}
    //protected bool isClosePress = false;
    //protected void FilterClose()
    //{
    //    isClosePress = true;
    //    IsFilterPopupOpen = false;
    //    isClosePress = false;
    //}

    private FilterSet _FilterSet;
    public FilterSet FilterSet
    {
        get => _FilterSet;
        set
        {
            Set(ref _FilterSet, value);
        }
    }


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

    public FilterPanelViewModel()
    {
        modelDate = new FilterSectionViewModel();
        modelDate.onChange += FilterDateOnChangeHandler;
        modelDate.Init("Период:", RetPeriodTreeFamilies(), TreeInitType.Last);
        FilterDate = new FilterSection(modelDate);

        modelCC = new FilterSectionViewModel();
        modelCC.onChange += FilterCCOnChangeHandler;
        modelCC.Init("Центры затрат:", RetCCTreeFamilies(), TreeInitType.All);
        FilterCC = new FilterSection(modelCC);

        modelER = new FilterSectionViewModel();
        modelER.onChange += FilterEROnChangeHandler;
        modelER.Init("Энергоресурсы:", RetERTreeFamilies(), TreeInitType.All);
        FilterER = new FilterSection(modelER);
    }
    private ObservableCollection<TreeFamily> RetCCTreeFamilies()
    {
        CostCenter cc = new CostCenter();
        ObservableCollection<TreeFamily> result = new ObservableCollection<TreeFamily>();
        result.Add(new TreeFamily()
        {
            Name = "Основные",
            Members = PList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.True, SelectedTechnology: SelectChoise.True))
        });
        result.Add(new TreeFamily()
        {
            Name = "Прочие технологические",
            Members = PList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.False, SelectedTechnology: SelectChoise.True))
        });
        result.Add(new TreeFamily()
        {
            Name = "Вспомогательные",
            Members = PList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.False, SelectedTechnology: SelectChoise.False))
        });
        return result;
    }
    private ObservableCollection<TreeFamily> RetERTreeFamilies()
    {
        EnergyResource er = new EnergyResource();

        ObservableCollection<TreeFamily> rez = new ObservableCollection<TreeFamily>();
        rez.Add(new TreeFamily()
        {
            Name = "Первичные энергоресурсы",
            Members = PList(er.Get(SelectedActual: SelectChoise.All, SelectedPrime: SelectChoise.True))
        });
        rez.Add(new TreeFamily()
        {
            Name = "Вторичные энергоресурсы",
            Members = PList(er.Get(SelectedActual: SelectChoise.All, SelectedPrime: SelectChoise.False))
        });
        return rez;
    }
    private ObservableCollection<TreeFamily> RetPeriodTreeFamilies()
    {
        int periodFirst = Period.MinPeriod;
        int startYear = Period.MinYear;
        int lastPeriod = Period.MaxPeriod;
        int lastYear = Period.MaxYear;
        int lastMonth = Period.MaxMonth;
        int[] arrYear = new int[lastYear - startYear + 1];
        for (int i = 0; i < (lastYear - startYear + 1); i++)
        {
            arrYear[i] = startYear + i;
        }
        ObservableCollection<TreeFamily> resultFamilies = new ObservableCollection<TreeFamily>();
        for (int y = startYear; y < lastYear; y++)
        {
            resultFamilies.Add(new TreeFamily()
            {
                Name = y.ToString(),
                Members = PList1(y)
            });
        }
        resultFamilies.Add(new TreeFamily()
        {
            Name = lastYear.ToString(),
            Members = PList2(lastYear)
        });
        return resultFamilies;

        List<TreePerson> PList1(int year)
        {
            List<TreePerson> resultPersons = new List<TreePerson>();
            for (int m = 1; m <= 12; m++)
            {
                resultPersons.Add(new TreePerson()
                {
                    Id = year * 100 + m,
                    Name = year.ToString() + " " + Period.monthArray[m - 1]
                });
            }
            return resultPersons;
        }
        List<TreePerson> PList2(int year)
        {
            List<TreePerson> rez2 = new List<TreePerson>();
            for (int m = 1; m <= lastMonth; m++)
            {
                rez2.Add(new TreePerson()
                {
                    Id = year * 100 + m,
                    Name = year.ToString() + " " + Period.monthArray[m - 1]
                });
            }
            return rez2;
        }
    }
    private List<TreePerson> PList<T>(List<T> tList)
    {
        List<IdName> ids = new List<IdName>((IEnumerable<IdName>)tList);
        List<TreePerson> result = new List<TreePerson>();
        foreach (IdName r in ids)
        {
            TreePerson n = new TreePerson();
            n.Id = r.Id;
            n.Name = r.Name;
            result.Add(n);
        }
        return result;
    }


    private void FilterDateOnChangeHandler()
    {
        FiltersIsChanged = true;
        filterDateList = modelDate.FilterList;
    }
    private void FilterCCOnChangeHandler()
    {
        FiltersIsChanged = true;
        filterCCList = modelCC.FilterList;
    }
    private void FilterEROnChangeHandler()
    {
        FiltersIsChanged = true;
        filterERList = modelER.FilterList;
    }


}
