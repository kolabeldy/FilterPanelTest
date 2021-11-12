namespace FilterPanelTest.Filter.Partials;
public partial class FilterPeriod : UserControl
{
    public FilterPeriod(FilterPeriodViewModel model)
    {
        InitializeComponent();
        DataContext = model;
    }
}
