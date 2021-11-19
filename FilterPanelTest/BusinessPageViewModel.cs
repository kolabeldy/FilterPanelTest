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
    private List<TreeNode> periodsTree = new List<TreeNode>();
    private List<TreeNode> ccTree = new List<TreeNode>();
    private List<TreeNode> erTree = new List<TreeNode>();

    public FilterPanel FilterPanel { get; set; }
    public BusinessPageViewModel()
    {
        periodsTree = PeriodTree();
        ccTree = CCTree();
        erTree = ERTree();
        List<TreeFilterCollection> treeFilterCollections = new();
        treeFilterCollections.Add(new TreeFilterCollection { FilterCollection = periodsTree, Title = "Период:", InitType = TreeInitType.Last });
        treeFilterCollections.Add(new TreeFilterCollection { FilterCollection = ccTree, Title = "Центры затрат:", InitType = TreeInitType.All });
        treeFilterCollections.Add(new TreeFilterCollection { FilterCollection = erTree, Title = "Энергоресурсы:", InitType = TreeInitType.All });
        FilterPanel = new FilterPanel(treeFilterCollections);
        FilterPanel.ViewModel.onChange += FiltersOnChangeHandler;
    }

    #region Подготовка коллекция для передачи в модуль Фильтр

    private List<TreeNode> PeriodTree()
    {
        List<TreeNode> result = new();
        //var descYears = from u in Period.Years
        //                orderby u descending
        //                select u;
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

    protected List<IdName> GetItemFilters(List<TreeNode> treeSource)
    {
        List<IdName> result = new ();
        foreach (TreeNode family in treeSource)
            foreach (TreeItem person in family.TreeNodeItems)
                if (ItemHelper.GetIsChecked(person) == true)
                {
                    result.Add(new IdName() { Id = person.Id, Name = person.Name });
                }
        return result;
    }

    private void FiltersOnChangeHandler() // Получение итоговых наборов фильтров по Period, CC, ER
    {
        List<IdName> periodsFilterList = GetItemFilters(periodsTree);
        List<IdName> ccFilterList = GetItemFilters(ccTree);
        List<IdName> erFilterList = GetItemFilters(erTree);
    }



}