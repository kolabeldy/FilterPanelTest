namespace FilterPanelTest;
public partial class MainWindow : Window
{
    private MainWindowModel viewmodel;
    public MainWindow()
    {
        viewmodel = new();
        DataContext = viewmodel;
        InitializeComponent();
    }

}