namespace FilterPanelTest.FilterTree;
public partial class FilterSection : UserControl
{
    public FilterSectionViewModel model;
    public FilterSection(FilterSectionViewModel model)
    {
        this.model = model;
        InitializeComponent();
        DataContext = model;
    }

}