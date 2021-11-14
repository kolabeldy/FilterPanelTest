﻿namespace FilterPanelTest.FilterTree.Base;
public enum TreeInitType { First, Last, All, None, Losses }

public abstract class FilterSectionViewModel : INotifyPropertyChanged
{
    public delegate void IsChangeMetodContainer();
    public event IsChangeMetodContainer onChange;


    #region Properties
    public ObservableCollection<TreeFamily> Families { get; set; }
    public string Tittle { get; set; }
    public List<TreePerson> FilterList { get; set; }
    #endregion

    #region Observable Properties

    private bool popupIsOpen = false;
    public bool PopupIsOpen
    {
        get
        {
            return popupIsOpen;
        }
        set
        {
            popupIsOpen = value;
            if (value == false)
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
            }
            OnPropertyChanged("PopupIsOpen");
        }
    }

    private bool isFilterSectionEnabled = true;
    public bool IsFilterSectionEnabled
    {
        get
        {
            return isFilterSectionEnabled;
        }
        set
        {
            isFilterSectionEnabled = value;
            OnPropertyChanged("IsFilterSectionEnabled");
        }
    }
    private Visibility toggleButtonCheckedAllIsVisible = Visibility.Visible;
    public Visibility ToggleButtonCheckedAllIsVisible
    {
        get
        {
            return toggleButtonCheckedAllIsVisible;
        }
        set
        {
            toggleButtonCheckedAllIsVisible = value;
            OnPropertyChanged("ToggleButtonCheckedAllIsVisible");
        }
    }
    private Visibility isFilterSectionVisible = Visibility.Visible;
    public Visibility IsFilterSectionVisible
    {
        get
        {
            return isFilterSectionVisible;
        }
        set
        {
            isFilterSectionVisible = value;
            OnPropertyChanged("IsFilterSectionVisible");
        }
    }

    private double popupOffset = 0;
    public double PopupHorisontalOffset
    {
        get
        {
            return popupOffset;
        }
        set
        {
            popupOffset = value;
            OnPropertyChanged("PopupOffset");
        }
    }

    private string selectedText;
    public string SelectedText
    {
        get
        {
            return selectedText;
        }
        set
        {
            selectedText = value;
            OnPropertyChanged("SelectedText");
        }
    }

    #endregion

    #region private Fields

    protected TreeInitType treeInitType;
    private int filterListAllCount;

    #endregion
    public FilterSectionViewModel(string tittle, TreeInitType treeInitType)
    {
        Families = new ObservableCollection<TreeFamily>();
        FilterList = new List<TreePerson>();
        this.treeInitType = treeInitType;
        this.Tittle = tittle;
    }

    #region Methods
    public void Init()
    {
        Families = FamiliesInit(RetFamilies());
        FilterList = PersonListFill();
        SelectedText = RetSelected();
        onChange();
    }

    public ObservableCollection<TreeFamily> FamiliesInit(ObservableCollection<TreeFamily> families)
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
    public abstract ObservableCollection<TreeFamily> RetFamilies();

    protected List<TreePerson> PersonListFill()
    {
        List<TreePerson> result = new List<TreePerson>();
        foreach (TreeFamily family in Families)
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
    protected void SelectAll()
    {
        foreach (TreeFamily family in Families)
            foreach (var person in family.Members)
            {
                ItemHelper.SetIsChecked(person, true);
            }
    }
    protected void UnselectAll()
    {
        foreach (TreeFamily family in Families)
            foreach (var person in family.Members)
            {
                ItemHelper.SetIsChecked(person, false);
            }
    }

    private RelayCommand selectAll_Command;
    public RelayCommand SelectAll_Command
    {
        get
        {
            return selectAll_Command ??
                (selectAll_Command = new RelayCommand(obj =>
                {
                    SelectAll();
                        //UnselectAll();
                    }));
        }
    }
    private RelayCommand unselectAll_Command;
    public RelayCommand UnselectAll_Command
    {
        get
        {
            return unselectAll_Command ??
                (unselectAll_Command = new RelayCommand(obj =>
                {
                        //SelectAll();
                        UnselectAll();
                }));
        }
    }



    #endregion

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

    #endregion
}
