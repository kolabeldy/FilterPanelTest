namespace FilterPanelTest.Models;
public class CostCenter : IdName, IDBModel
{
    public bool IsMain { get; set; }
    public bool IsActual { get; set; }
    public bool IsTechnology { get; set; }

    public SelectChoise SelectedActual { get; set; } = SelectChoise.True;
    public SelectChoise SelectedMain { get; set; } = SelectChoise.All;
    public SelectChoise SelectedTechnology { get; set; } = SelectChoise.All;
    public List<CostCenter> SelectedList { get; set; } = new();

    public static List<CostCenter> TechMainList { get; set; } = new List<CostCenter>();
    public static List<CostCenter> TechOtherList { get; set; } = new List<CostCenter>();
    public static List<CostCenter> SlaveList { get; set; } = new List<CostCenter>();

    #region Methods
    public void Init()
    {
        CostCenter ccAll = new CostCenter { Id = 0, Name = "все" };
        TechMainList.Add(ccAll);
        TechMainList.AddRange(Get(SelectChoise.True, SelectChoise.True, SelectChoise.True));
        TechOtherList.Add(ccAll);
        TechOtherList.AddRange(Get(SelectChoise.True, SelectChoise.False, SelectChoise.True));
        SlaveList.Add(ccAll);
        SlaveList.AddRange(Get(SelectChoise.True, SelectChoise.False, SelectChoise.False));

    }
    public List<CostCenter> Get(SelectChoise SelectedActual, SelectChoise SelectedMain, SelectChoise SelectedTechnology)
    {
        this.SelectedActual = SelectedActual;
        this.SelectedMain = SelectedMain;
        this.SelectedTechnology = SelectedTechnology;
        return Get<CostCenter>();
    }
    public List<T> Get<T>()
    {
        List<T> result = new();
        List<CostCenter> list = new();

        string isActualStr = SelectedActual == SelectChoise.All ? "" : SelectedActual == SelectChoise.True ? " AND IsActual = 1" : " AND IsActual = 0";
        string isMainStr = SelectedMain == SelectChoise.All ? "" : SelectedMain == SelectChoise.True ? " AND IsMain = 1" : " AND IsMain = 0"; ;
        string isTechnologyStr = SelectedTechnology == SelectChoise.All ? "" : SelectedTechnology == SelectChoise.True ? " AND isTechnology = 1" : " AND isTechnology = 0";
        string whereStr = "WHERE True" + isActualStr + isMainStr + isTechnologyStr;
        string sql = "SELECT Id, Name, IsMain, IsActual, IsTechnology FROM CostCenters " + whereStr + " ORDER BY Id";


        //var resourceManager = Properties.Resources.ResourceManager;
        //string sql = resourceManager.GetString("sqlCostCenters_ToList").Replace("#whereStr", whereStr);

        DataTable dt = new DataTable();
        dt = Sqlite.Select(sql);
        list = (from DataRow dr in dt.Rows
                select new CostCenter()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    IsActual = Convert.ToBoolean(dr["IsActual"]),
                    IsMain = Convert.ToBoolean(dr["IsMain"]),
                    IsTechnology = Convert.ToBoolean(dr["IsTechnology"]),
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }
    public int Add(object rec)
    {
        if (rec == null) return -1;
        CostCenter record = rec as CostCenter;
        string sql = "INSERT INTO CostCenters (Id, Name, IsMain, IsTechnology, IsActual) VALUES ("
                        + record.Id.ToString() + ", '" + record.Name + "'" + ", " + record.IsMain.ToString() + ", "
                        + record.IsTechnology.ToString() + ", " + record.IsActual.ToString() + ")";
        return Sqlite.ExecNonQuery(sql);
    }
    public int Delete(string whereStr)
    {
        string sql = "Delete FROM CostCenters WHERE " + whereStr;
        return Sqlite.ExecNonQuery(sql);
    }
    public int Update(object rec)
    {
        if (rec == null) return -1;
        CostCenter record = rec as CostCenter;
        string sql = "UPDATE CostCenters SET (Name, IsMain, IsTechnology, IsActual) = ("
                        + "'" + record.Name + "'" + ", " + record.IsMain.ToString()
                        + ", " + record.IsTechnology.ToString() + ", " + record.IsActual.ToString() + ")"
                        + "WHERE Id = " + record.Id.ToString();

        return Sqlite.ExecNonQuery(sql);
    }
    #endregion
}
