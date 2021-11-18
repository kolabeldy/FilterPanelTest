namespace FilterPanelTest;
public class BusinessPageViewModel : BaseViewModel
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
    public BusinessPageViewModel()
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
        List<TreeNode> result = new();

        foreach (int r in Period.Years)
        {
            result.Add(new TreeNode()
            {
                Name = r.ToString(),
                TreeNodeItems = ConvertToTreeItemList(Period.YearPeriods(r))
            });
        }
        return result;
    }
    private List<TreeNode> CCTree()
    {
        CostCenter cc = new CostCenter();
        List<TreeNode> result = new()
        {
            new TreeNode()
            {
                Name = "Основные",
                TreeNodeItems = ConvertToTreeItemList(CostCenter.TechMainList)
            },
            new TreeNode()
            {
                Name = "Прочие технологические",
                TreeNodeItems = ConvertToTreeItemList(CostCenter.TechOtherList)
            },
            new TreeNode()
            {
                Name = "Вспомогательные",
                TreeNodeItems = ConvertToTreeItemList(CostCenter.SlaveList)
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
                Name = "Первичные",
                TreeNodeItems = ConvertToTreeItemList(EnergyResource.PrimeList)
            },
            new TreeNode()
            {
                Name = "Вторичные",
                TreeNodeItems = ConvertToTreeItemList(EnergyResource.SecondaryList)
            }
        };
        return result;
    }
    private List<TreeItem> ConvertToTreeItemList<T>(List<T> tList)
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