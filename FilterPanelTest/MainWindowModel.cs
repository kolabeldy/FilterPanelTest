using FilterPanelTest.Filter;

namespace FilterPanelTest;
public class MainWindowModel : BaseViewModel
{
    private string _Caption = "Тест панели фильтров";
    public string Caption
    {
        get => _Caption;
        set
        {
            Set(ref _Caption, value);
        }
    }
    public FilterPanel FilterPanel {get;set;}

    public MainWindowModel()
    {
        FilterPanel = new FilterPanel();
    }


}