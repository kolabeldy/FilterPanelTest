namespace FilterPanelTest.FilterTree;
public partial class FilterPanel : UserControl
{
    private FilterPanelViewModel viewmodel;
    public FilterPanel()
    {
        viewmodel = new FilterPanelViewModel();
        DataContext = viewmodel;
        InitializeComponent();
    }
}