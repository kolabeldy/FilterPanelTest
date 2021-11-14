namespace FilterPanelTest;
public partial class MainWindow : Window
{
    private MainWindowModel model;
    public MainWindow()
    {
        model = new(periodVisible: true, ccVisible: true, erVisible: true, ntVisible: true);
        DataContext = model;
        InitializeComponent();
    }

}