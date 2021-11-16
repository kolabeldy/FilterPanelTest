namespace FilterPanelTest.Models;

public class Use
{
    private IEnumerable<Period> SelectedPeriods { get; set; }
    private IEnumerable<CostCenter> SelectedCostCenters { get; set; }
    private IEnumerable<EnergyResource> SelectedEnergyResources { get; set; }
    public IEnumerable<UseValue> SelectedUseValues { get; set; }

    public Use(IEnumerable<Period> _SelectedPeriods, IEnumerable<CostCenter> _SelectedCostCenters, IEnumerable<EnergyResource> _SelectedEnergyResources)
    {
        SelectedPeriods = _SelectedPeriods;
        SelectedCostCenters = _SelectedCostCenters;
        SelectedEnergyResources = _SelectedEnergyResources;
    }
    public List<UseValue>? Get()
    {
        string _wherePeriods = "( ";
        foreach (var r in SelectedPeriods)
        {
            _wherePeriods += r.Id + ", ";
        }
        string wherePeriods = _wherePeriods.Substring(0, _wherePeriods.Length - 2) + " )";
        string _whereCC = "( ";
        foreach (var r in SelectedCostCenters)
        {
            _whereCC += r.Id + ", ";
        }
        string whereCC = _whereCC.Substring(0, _whereCC.Length - 2) + " )";
        string _whereER = "( ";
        foreach (var r in SelectedEnergyResources)
        {
            _whereER += r.Id + ", ";
        }
        string whereER = _whereER.Substring(0, _whereER.Length - 2) + " )";
        string whereStr = "WHERE Period IN " + wherePeriods + " AND IdCC IN " + whereCC + " AND IdER IN " + whereER;

        string sql = "SELECT Period, IdCC, IdProduct, ProductName, IdER, ERFact, ERPlan FROM ERUses " + whereStr + " ORDER BY Period";

        DataTable dt = new DataTable();
        dt = Sqlite.Select(Global.dbpath, sql);

        return (from DataRow dr in dt.Rows
                select new UseValue()
                {
                    Period = new() { Id = Convert.ToInt32(dr["Period"]) },
                    CostCenter = new() { Id = Convert.ToInt32(dr["IdCC"]) },
                    IdProduct = Convert.ToInt32(dr["IdProduct"]),
                    ProductName = dr["ProductName"].ToString(),
                    EnergyResource = new() { Id = Convert.ToInt32(dr["IdER"]) },
                    Fact = Convert.ToDouble(dr["ERFact"]),
                    Plan = Convert.ToDouble(dr["ERPlan"])
                }).ToList();
    }


}
