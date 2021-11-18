namespace FilterPanelTest;
public class MainWindowModel : BaseViewModel
{
    private string _Caption = "Тест панели фильтров";
    public string Caption
    {
        get => _Caption;
        set
        {
            Set(ref _Caption, value);
        }
    }

    public FilterPanel FilterPanel {get;set;}
    public MainWindowModel()
    {
        List<TreeFilterCollection> _TreeFilterCollections = new();
        _TreeFilterCollections.Add(new TreeFilterCollection { FilterCollection = RetPeriodTreeFamilies(), Title = "Период:", InitType = TreeInitType.Last });
        _TreeFilterCollections.Add(new TreeFilterCollection { FilterCollection = RetCCTreeFamilies(), Title = "Центры затрат:", InitType = TreeInitType.All });
        _TreeFilterCollections.Add(new TreeFilterCollection { FilterCollection = RetERTreeFamilies(), Title = "Энергоресурсы:", InitType = TreeInitType.All });
        FilterPanel = new FilterPanel(_TreeFilterCollections);
    }

    #region Create List<TreeFamily> FilterTree Datas

    private List<TreeNode> RetPeriodTreeFamilies()
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
        List<TreeNode> resultFamilies = new List<TreeNode>();
        for (int y = startYear; y < lastYear; y++)
        {
            resultFamilies.Add(new TreeNode()
            {
                Name = y.ToString(),
                Members = PList1(y)
            });
        }
        resultFamilies.Add(new TreeNode()
        {
            Name = lastYear.ToString(),
            Members = PList2(lastYear)
        });
        return resultFamilies;

        List<TreeItem> PList1(int year)
        {
            List<TreeItem> resultPersons = new List<TreeItem>();
            for (int m = 1; m <= 12; m++)
            {
                resultPersons.Add(new TreeItem()
                {
                    Id = year * 100 + m,
                    Name = year.ToString() + " " + Period.monthArray[m - 1]
                });
            }
            return resultPersons;
        }
        List<TreeItem> PList2(int year)
        {
            List<TreeItem> rez2 = new List<TreeItem>();
            for (int m = 1; m <= lastMonth; m++)
            {
                rez2.Add(new TreeItem()
                {
                    Id = year * 100 + m,
                    Name = year.ToString() + " " + Period.monthArray[m - 1]
                });
            }
            return rez2;
        }
    }
    private List<TreeNode> RetCCTreeFamilies()
    {
        CostCenter cc = new CostCenter();
        List<TreeNode> result = new List<TreeNode>();
        result.Add(new TreeNode()
        {
            Name = "Основные",
            Members = PList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.True, SelectedTechnology: SelectChoise.True))
        });
        result.Add(new TreeNode()
        {
            Name = "Прочие технологические",
            Members = PList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.False, SelectedTechnology: SelectChoise.True))
        });
        result.Add(new TreeNode()
        {
            Name = "Вспомогательные",
            Members = PList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.False, SelectedTechnology: SelectChoise.False))
        });
        return result;
    }
    private List<TreeNode> RetERTreeFamilies()
    {
        EnergyResource er = new EnergyResource();

        List<TreeNode> rez = new List<TreeNode>();
        rez.Add(new TreeNode()
        {
            Name = "Первичные энергоресурсы",
            Members = PList(er.Get(SelectedActual: SelectChoise.All, SelectedPrime: SelectChoise.True))
        });
        rez.Add(new TreeNode()
        {
            Name = "Вторичные энергоресурсы",
            Members = PList(er.Get(SelectedActual: SelectChoise.All, SelectedPrime: SelectChoise.False))
        });
        return rez;
    }
    private List<TreeItem> PList<T>(List<T> tList)
    {
        List<IdName> ids = new List<IdName>((IEnumerable<IdName>)tList);
        List<TreeItem> result = new List<TreeItem>();
        foreach (IdName r in ids)
        {
            TreeItem n = new TreeItem();
            n.Id = r.Id;
            n.Name = r.Name;
            result.Add(n);
        }
        return result;
    }

    #endregion


}