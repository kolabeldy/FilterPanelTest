namespace FilterPanelTest;
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    private void MenuButton_Click(object sender, RoutedEventArgs e)
    {
        BusinessPage businessPage = BusinessPage.GetInstance();
        BusinessPageFrame.Content = businessPage;
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        BusinessPageFrame.Content = null;
    }
}