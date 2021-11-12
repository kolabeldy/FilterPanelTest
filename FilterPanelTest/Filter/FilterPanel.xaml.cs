namespace FilterPanelTest.Filter;
public partial class FilterPanel : UserControl
{
    private FilterPanelViewModel model;
    public FilterPanel(FilterPanelViewModel model)
    {
        this.model = model;
        InitializeComponent();
        DataContext = model;
    }
}
