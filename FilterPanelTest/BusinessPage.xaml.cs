namespace FilterPanelTest;
public partial class BusinessPage : UserControl
{
    private static BusinessPage instance;
    public static BusinessPage GetInstance()
    {
        if (instance == null)
        {
            instance = new BusinessPage();
        }
        return instance;
    }

    private BusinessPageViewModel viewmodel;
    private BusinessPage()
    {
        viewmodel = new();
        DataContext = viewmodel;
        InitializeComponent();
    }

}