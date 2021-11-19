namespace FilterPanelTest.Models;

public enum MonthOutputStyle { AsNumeric, AsString };
public class Period : IdName, IDBModel
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; }

    public static int MinPeriod { get; set; }
    public static int MaxPeriod { get; set; }
    public static int MinYear { get; set; }
    public static int MaxYear { get; set; }
    public static int MinMonth { get; set; }
    public static int MaxMonth { get; set; }
    public static List<Period> Periods { get; set; }
    public static List<int> Years { get; set; }

    public Period()
    {
    }

    private List<Period> GetPeriods()
    {
        return Get<Period>();
    }

    public void Init()
    {
        Periods = Get<Period>();
        Years = GetYears();
    }

    private static List<int> GetYears()
    {
        return (from o in Periods
                group o by new { o.Year } into gr
                select gr.Key.Year).ToList();
    }
    public static List<IdName> YearPeriods(int year)
    {
        return (from o in Periods
                where o.Year == year
                select new IdName
                {
                    Id = o.Id,
                    Name = o.Name
                }).ToList();
    }

    public static string[] monthArray = new string[]
    { "янв", "фев", "мар", "апр", "май", "июн", "июл", "авг", "сен", "окт", "ноя", "дек" };

    #region IDBModel realisattion

    public List<T> Get<T>()
    {
        List<T> result = new();
        List<Period> list = new();

        string sql = "SELECT Id, Name, YearId, MonthId FROM Periods";

        DataTable dt = new DataTable();
        dt = Sqlite.Select(Global.dbpath, sql);
        list = (from DataRow dr in dt.Rows
                select new Period()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Year = Convert.ToInt32(dr["YearId"]),
                    Month = Convert.ToInt32(dr["MonthId"])
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }
    public int Add(object rec)
    {
        //if (rec == null) return -1;
        //CostCenter record = rec as CostCenter;
        //string sql = "INSERT INTO CostCenters (Id, Name, IsMain, IsTechnology, IsActual) VALUES ("
        //                + record.Id.ToString() + ", '" + record.Name + "'" + ", " + record.IsMain.ToString() + ", "
        //                + record.IsTechnology.ToString() + ", " + record.IsActual.ToString() + ")";
        //return Sqlite.ExecNonQuery(Global.dbpath, sql);
        return 0;
    }
    public int Delete(string whereStr)
    {
        //string sql = "Delete FROM CostCenters WHERE " + whereStr;
        //return Sqlite.ExecNonQuery(Global.dbpath, sql);
        return 0;
    }
    public int Update(object rec)
    {
        //if (rec == null) return -1;
        //CostCenter record = rec as CostCenter;
        //string sql = "UPDATE CostCenters SET (Name, IsMain, IsTechnology, IsActual) = ("
        //                + "'" + record.Name + "'" + ", " + record.IsMain.ToString()
        //                + ", " + record.IsTechnology.ToString() + ", " + record.IsActual.ToString() + ")"
        //                + "WHERE Id = " + record.Id.ToString();

        //return Sqlite.ExecNonQuery(Global.dbpath, sql);
        return 0;
    }

    #endregion

}
