namespace FilterPanelTest.Models;
public class DataUse
{
    //public long Id { get; set; }
    public string Name { get; set; }
    public int Period { get; set; }
    public int IdCC { get; set; }
    public string CCName { get; set; }
    public bool IsNorm { get; set; }
    public int IdProduct { get; set; }
    public string ProductName { get; set; }
    public int IdER { get; set; }
    public string ERName { get; set; }
    public string UnitName { get; set; }
    public double Fact { get; set; }
    public double Plan { get; set; }
    public double Diff { get; set; }
    public double FactCost { get; set; }
    public double PlanCost { get; set; }
    public double DiffCost { get; set; }
    public double DiffProc { get; set; }

    public static List<DataUse> GetER()
    {
        return GetER<DataUse>();
    }
    public static List<T> GetER<T>()
    {
        List<T> result = new();
        List<DataUse> list = new();
        string sql = "SELECT IdER, ERName, UnitName, Fact, Plan, Diff, FactCost, PlanCost, DiffCost FROM MonitorUseGroupBy_IdER ORDER BY IdER";
        //var resourceManager = Properties.Resources.ResourceManager;
        DataTable dt = new DataTable();
        dt = Sqlite.Select(sql);
        list = (from DataRow dr in dt.Rows
                select new DataUse()
                {
                    IdER = Convert.ToInt32(dr["IdER"]),
                    ERName = dr["ERName"].ToString(),
                    UnitName = dr["UnitName"].ToString(),
                    Fact = Convert.ToDouble(dr["Fact"]),
                    Plan = Convert.ToDouble(dr["Plan"]),
                    Diff = Convert.ToDouble(dr["Diff"]),
                    FactCost = Convert.ToDouble(dr["FactCost"]),
                    PlanCost = Convert.ToDouble(dr["PlanCost"]),
                    DiffCost = Convert.ToDouble(dr["DiffCost"]),
                    //DiffProc = Convert.ToDouble(dr["DiffProc"]),
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }
    public static List<DataUse> GetCC()
    {
        return GetCC<DataUse>();
    }
    public static List<T> GetCC<T>()
    {
        List<T> result = new();
        List<DataUse> list = new();
        string sql = "SELECT * FROM AnalysisUseFromSelected_Period_CC ORDER BY IdCC, IsNorm DESC";
        //var resourceManager = Properties.Resources.ResourceManager;
        DataTable dt = new DataTable();
        dt = Sqlite.Select(sql);
        list = (from DataRow dr in dt.Rows
                select new DataUse()
                {
                    IdCC = Convert.ToInt32(dr["IdCC"]),
                    CCName = dr["CCName"].ToString(),

                    IsNorm = Convert.ToBoolean(dr["IsNorm"]),
                    IdProduct = Convert.ToInt32(dr["IdProduct"]),
                    ProductName = dr["ProductName"].ToString(),

                    IdER = Convert.ToInt32(dr["IdER"]),
                    ERName = dr["ERName"].ToString(),
                    UnitName = dr["UnitName"].ToString(),
                    Fact = Convert.ToDouble(dr["Fact"]),
                    Plan = Convert.ToDouble(dr["Plan"]),
                    Diff = Convert.ToDouble(dr["Diff"]),

                    FactCost = Convert.ToDouble(dr["FactCost"]),
                    PlanCost = Convert.ToDouble(dr["PlanCost"]),
                    DiffCost = Convert.ToDouble(dr["DiffCost"]),
                    DiffProc = Convert.ToDouble(dr["DiffCost"]) * 100 / Convert.ToDouble(dr["PlanCost"])
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }

    public static (int, int) GetMinMaxPeriods()
    {
        string sql = "SELECT MinPeriod, MaxPeriod FROM MinMaxPeriods";
        DataTable dt = new DataTable();
        dt = Sqlite.Select(sql);
        int minPeriod = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
        int maxPeriod = Convert.ToInt32(dt.Rows[0].ItemArray[1].ToString());
        return (minPeriod, maxPeriod);
    }


}
