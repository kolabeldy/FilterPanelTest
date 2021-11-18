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
        List<TreeFilterCollection> treeFilterCollections = new();
        treeFilterCollections.Add(new TreeFilterCollection { FilterCollection = PeriodTree(), Title = "Период:", InitType = TreeInitType.Last });
        treeFilterCollections.Add(new TreeFilterCollection { FilterCollection = CCTree(), Title = "Центры затрат:", InitType = TreeInitType.All });
        treeFilterCollections.Add(new TreeFilterCollection { FilterCollection = ERTree(), Title = "Энергоресурсы:", InitType = TreeInitType.All });
        FilterPanel = new FilterPanel(treeFilterCollections);
    }

    #region Create List<TreeFamily> FilterTree Datas

    private List<TreeNode> PeriodTree()
    {
        int startYear = Period.MinYear;
        int lastYear = Period.MaxYear;
        int lastMonth = Period.MaxMonth;
        int[] arrYear = new int[lastYear - startYear + 1];
        for (int i = 0; i < (lastYear - startYear + 1); i++)
        {
            arrYear[i] = startYear + i;
        }
        List<TreeNode> result = new();
        for (int y = startYear; y < lastYear; y++)
        {
            result.Add(new TreeNode()
            {
                Name = y.ToString(),
                TreeNodeItems = PList1(y)
            });
        }
        result.Add(new TreeNode()
        {
            Name = lastYear.ToString(),
            TreeNodeItems = PList2(lastYear)
        });
        return result;

        List<TreeItem> PList1(int year)
        {
            List<TreeItem> result1 = new List<TreeItem>();
            for (int m = 1; m <= 12; m++)
            {
                result1.Add(new TreeItem()
                {
                    Id = year * 100 + m,
                    Name = year.ToString() + " " + Period.monthArray[m - 1]
                });
            }
            return result1;
        }
        List<TreeItem> PList2(int year)
        {
            List<TreeItem> result2 = new List<TreeItem>();
            for (int m = 1; m <= lastMonth; m++)
            {
                result2.Add(new TreeItem()
                {
                    Id = year * 100 + m,
                    Name = year.ToString() + " " + Period.monthArray[m - 1]
                });
            }
            return result2;
        }
    }
    private List<TreeNode> CCTree()
    {
        CostCenter cc = new CostCenter();
        List<TreeNode> result = new()
        {
            new TreeNode()
            {
                Name = "Основные",
                TreeNodeItems = GetTreeItemList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.True, SelectedTechnology: SelectChoise.True))
            },
            new TreeNode()
            {
                Name = "Прочие технологические",
                TreeNodeItems = GetTreeItemList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.False, SelectedTechnology: SelectChoise.True))
            },
            new TreeNode()
            {
                Name = "Вспомогательные",
                TreeNodeItems = GetTreeItemList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.False, SelectedTechnology: SelectChoise.False))
            }
        };
        return result;
    }
    private List<TreeNode> ERTree()
    {
        EnergyResource er = new EnergyResource();

        List<TreeNode> result = new()
        {
            new TreeNode()
            {
                Name = "Первичные энергоресурсы",
                TreeNodeItems = GetTreeItemList(er.Get(SelectedActual: SelectChoise.All, SelectedPrime: SelectChoise.True))
            },
            new TreeNode()
            {
                Name = "Вторичные энергоресурсы",
                TreeNodeItems = GetTreeItemList(er.Get(SelectedActual: SelectChoise.All, SelectedPrime: SelectChoise.False))
            }
        };
        return result;
    }
    private List<TreeItem> GetTreeItemList<T>(List<T> tList)
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