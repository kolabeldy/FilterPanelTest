namespace FilterPanelTest.FilterTree;
public partial class FilterPanel : UserControl
{
    private FilterPanelViewModel viewmodel;
    public FilterPanel(List<TreeFilterCollection> treeFilterCollections)
    {
        viewmodel = new FilterPanelViewModel(treeFilterCollections);
        DataContext = viewmodel;
        InitializeComponent();
    }
}