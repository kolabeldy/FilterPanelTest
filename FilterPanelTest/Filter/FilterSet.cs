namespace FilterPanelTest.Filter;
public struct FilterSet
{
    public int StartPeriod { get; set; }
    public DateTime StartDate { get; set; }
    public int EndPeriod { get; set; }
    public DateTime EndDate { get; set; }
    public int StartDynamicPeriod { get; set; }
    public int EndDynamicPeriod { get; set; }
    public List<CostCenter> SelectedCC { get; set; }
    public List<EnergyResource> SelectedER { get; set; }
    public int SelectedNormTypes { get; set; }
    public string TitleSelectedCCGroup { get; set; }
    public string TitleSelectedERGroup { get; set; }
    public string TitleSelectedIsNormGroup { get; set; }
}
