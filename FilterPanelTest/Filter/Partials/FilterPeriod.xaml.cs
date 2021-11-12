namespace FilterPanelTest.Filter.Partials;
public partial class FilterPeriod : UserControl
{
    public FilterPeriod(FilterPeriodViewModel model)
    {
        InitializeComponent();
        DataContext = model;
    }
    private void StartPeriodPicker_Opened(object sender, RoutedEventArgs e)
    {
        DatePicker datepicker = (DatePicker)sender;
        Popup popup = (Popup)datepicker.Template.FindName("PART_Popup", datepicker);
        Calendar cal = (Calendar)popup.Child;
        cal.DisplayModeChanged += CalenderStart_DisplayModeChanged;
        cal.DisplayMode = CalendarMode.Year;
    }
    private void EndPeriodPicker_Opened(object sender, RoutedEventArgs e)
    {
        DatePicker datepicker = (DatePicker)sender;
        Popup popup = (Popup)datepicker.Template.FindName("PART_Popup", datepicker);
        Calendar cal = (Calendar)popup.Child;
        cal.DisplayModeChanged += CalenderEnd_DisplayModeChanged;
        cal.DisplayMode = CalendarMode.Year;
    }
    private void CalenderStart_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
    {
        Calendar calendar = (Calendar)sender;
        if (calendar.DisplayMode == CalendarMode.Month)
        {
            DateTime d = calendar.DisplayDate;
            calendar.SelectedDate = calendar.DisplayDate;
            calendar.DisplayDate = new DateTime(d.Year, d.Month, 1);
            StartPeriodPicker.IsDropDownOpen = false;
        }
    }
    private void CalenderEnd_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
    {
        Calendar calendar = (Calendar)sender;
        if (calendar.DisplayMode == CalendarMode.Month)
        {
            DateTime d = calendar.DisplayDate;
            DateTime newdate = new DateTime(d.Year, d.Month, DateTime.DaysInMonth(d.Year, d.Month));
            calendar.SelectedDate = newdate;
            calendar.DisplayDate = newdate;
            EndPeriodPicker.IsDropDownOpen = false;
        }
    }
}
