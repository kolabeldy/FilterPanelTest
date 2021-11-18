namespace FilterPanelTest;
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                    new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

        Sqlite.PathDB = Global.dbpath;
        new Period().Init();
        new CostCenter().Init();
        new EnergyResource().Init();
        MainWindow window = new MainWindow();
        window.Show();
    }
}