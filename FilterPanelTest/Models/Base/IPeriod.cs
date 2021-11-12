namespace FilterPanelTest.Models.Base;
public interface IPeriod
{
    #region Properties
    int Id { get; set; }
    string Name { get; set; }
    string NameFull { get; set; }
    int Year { get; set; }
    int Month { get; set; }
    string MonthName { get; set; }
    DateTime SelectedStartDate { get; set; }
    DateTime SelectedEndDate { get; set; }
    int SelectedStartPeriod { get; set; }
    int SelectedEndPeriod { get; set; }
    int MinSelectedPeriod { get; set; }
    int MaxSelectedPeriod { get; set; }
    int MinDynamicSelectedPeriod { get; set; }
    int MaxDynamicSelectedPeriod { get; set; }
    List<Period> Periods { get; set; }

    #endregion

    #region StaticProperties
    static int MinPeriod { get; set; }
    static int MaxPeriod { get; set; }
    static int MinYear { get; set; }
    static int MaxYear { get; set; }
    static int MinMonth { get; set; }
    static int MaxMonth { get; set; }
    #endregion

    #region Methods
    void Init();
    void SetDynamicPeriods();

    #endregion

}
