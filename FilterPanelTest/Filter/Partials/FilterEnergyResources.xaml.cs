namespace FilterPanelTest.Filter.Partials;
public partial class FilterEnergyResources : UserControl
{
    public FilterEnergyResources(FilterEnergyResourcesViewModel model)
    {
        InitializeComponent();
        DataContext = model;
    }
}
