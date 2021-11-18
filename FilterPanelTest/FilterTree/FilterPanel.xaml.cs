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

    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        FilterSection filterSection = (sender as ListBox).SelectedItem as FilterSection;
        filterSection.PopupBox.IsPopupOpen = true;
        //filterSection.model.IsFilterPopupOpen = true;
    }
}