namespace FilterPanelTest.Filter.Partials;
public partial class FilterNormTypes : UserControl
{
    public FilterNormTypes(FilterNormTypesViewModel model)
    {
        InitializeComponent();
        DataContext = model;
    }
}
