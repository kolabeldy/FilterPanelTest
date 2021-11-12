namespace FilterPanelTest.Filter.Partials;
public partial class FilterCostCenters : UserControl
{
    public FilterCostCenters(FilterCostCentersViewModel model)
    {
        InitializeComponent();
        DataContext = model;
    }
}
