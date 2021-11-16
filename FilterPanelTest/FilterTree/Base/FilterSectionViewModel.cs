using MyControlsLibrary.CheckedTree;
using MyServicesLibrary.Helpers;

namespace FilterPanelTest.FilterTree.Base;
public enum TreeInitType { First, Last, All, None, Losses }

public abstract class FilterSectionViewModel : BaseViewModel
{
    public delegate void IsChangeMetodContainer();
    public event IsChangeMetodContainer onChange;


    #region Properties

    public ObservableCollection<TreeFamily> FilterTreeItems { get; set; }
    public string Tittle { get; set; } = "Title";
    public List<TreePerson> FilterList { get; set; }

    #endregion

    #region Observable Properties

    protected bool isClosePress = false;

    protected bool _IsFilterPopupOpen = false;
    public bool IsFilterPopupOpen
    {
        get => _IsFilterPopupOpen;
        set
        {
            if (isClosePress)
            {
                List<TreePerson> tmpList = new List<TreePerson>();
                tmpList = PersonListFill();
                if (!ListCompare(FilterList, tmpList))
                {
                    FilterList.Clear();
                    FilterList = tmpList;
                    SelectedText = RetSelected();
                    onChange();
                }
                Set(ref _IsFilterPopupOpen, value);
            }
            else Set(ref _IsFilterPopupOpen, true);
        }
    }

    protected string _SelectedText;
    public string SelectedText
    {
        get
        {
            return _SelectedText;
        }
        set
        {
            Set(ref _SelectedText, value);
        }
    }

    #endregion

    public FilterSectionViewModel()
    {
        FilterTreeItems = new ObservableCollection<TreeFamily>();
        FilterList = new List<TreePerson>();
    }

    #region Methods
    public void Init(string tittle = null, TreeInitType treeInitType = TreeInitType.All)
    {
        Tittle = tittle;
        FilterTreeItems = FamiliesInit(RetTreeFamilies(), treeInitType);
        FilterList = PersonListFill();
        SelectedText = RetSelected();
        onChange();
    }

    protected int filterListAllCount;
    public ObservableCollection<TreeFamily> FamiliesInit(ObservableCollection<TreeFamily> families, TreeInitType treeInitType = TreeInitType.All)
    {
        filterListAllCount = 0;
        foreach (TreeFamily family in families)
            foreach (var person in family.Members)
            {
                person.SetValue(ItemHelper.ParentProperty, family);
                filterListAllCount++;
            }
        int i = 1;
        foreach (TreeFamily family in families)
            foreach (var person in family.Members)
            {
                switch (treeInitType)
                {
                    case TreeInitType.First:
                        if (i > 1)
                        {
                            ItemHelper.SetIsChecked(person, false);
                        }
                        i++;
                        break;
                    case TreeInitType.Last:
                        if (i != filterListAllCount)
                        {
                            ItemHelper.SetIsChecked(person, false);
                        }
                        i++;
                        break;
                }
            }
        return families;
    }
    public abstract ObservableCollection<TreeFamily> RetTreeFamilies();

    protected List<TreePerson> PersonListFill()
    {
        List<TreePerson> result = new List<TreePerson>();
        foreach (TreeFamily family in FilterTreeItems)
            foreach (TreePerson person in family.Members)
                if (ItemHelper.GetIsChecked(person) == true)
                {
                    result.Add(new TreePerson() { Id = person.Id, Name = person.Name });
                }
        return result;
    }

    public string RetSelected()
    {
        string rez = "выборочно";

        int countList = FilterList.Count;
        if (countList == 1) rez = FilterList[0].Name;
        else if (countList == filterListAllCount) rez = "все";
        if (countList == 0) rez = "не выбрано";
        return rez;
    }
    public static bool ListCompare(List<TreePerson> List1, List<TreePerson> List2)
    {
        if (List1.Count == List2.Count)
        {
            for (int i = 0; i < List1.Count; i++)
            {
                if (List1[i].Id != List2[i].Id)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    protected RelayCommand _SelectAll_Command;
    public RelayCommand SelectAll_Command
    {
        get
        {
            return _SelectAll_Command ??
                (_SelectAll_Command = new RelayCommand(obj =>
                {
                    foreach (TreeFamily family in FilterTreeItems)
                        foreach (var person in family.Members)
                        {
                            ItemHelper.SetIsChecked(person, true);
                        }
                }));
        }
    }

    protected RelayCommand _UnselectAll_Command;
    public RelayCommand UnselectAll_Command
    {
        get
        {
            return _UnselectAll_Command ??
                (_UnselectAll_Command = new RelayCommand(obj =>
                {
                    foreach (TreeFamily family in FilterTreeItems)
                        foreach (var person in family.Members)
                        {
                            ItemHelper.SetIsChecked(person, false);
                        }
                }));
        }
    }

    protected RelayCommand _FilterPanelClose_Command;
    public RelayCommand FilterPanelClose_Command
    {
        get
        {
            return _FilterPanelClose_Command ??
                (_FilterPanelClose_Command = new RelayCommand(obj =>
                {
                    Close();
                }));
        }
    }
    protected void Close()
    {
        isClosePress = true;
        IsFilterPopupOpen = false;
        isClosePress = false;
    }

    #endregion
}

