namespace FilterPanelTest.Models;

public class EnergyResource : INotifyPropertyChanged, IDBModel
{
    public int Id { get; set; }

    private string _Name;
    public string Name { get => _Name; set => Set(ref _Name, value); }

    private string _NameFull;
    public string NameFull { get => _NameFull; set => Set(ref _NameFull, value); }
    public int IdGroup { get; set; }
    public string ShortName { get; set; }
    public int IdUnit { get; set; }
    public bool IsMain { get; set; }
    public bool IsActual { get; set; }
    public bool IsPrime { get; set; }

    public SelectChoise SelectedActual { get; set; } = SelectChoise.True;
    public SelectChoise SelectedPrime { get; set; } = SelectChoise.All;
    public static List<EnergyResource> SelectedList { get; set; } = new();

    public static List<EnergyResource> PrimeList { get; set; } = new List<EnergyResource>();
    public static List<EnergyResource> SecondaryList { get; set; } = new List<EnergyResource>();


    public void Init()
    {
        EnergyResource erAll = new EnergyResource { Id = 0, NameFull = "все" };
        PrimeList.Add(erAll);
        PrimeList.AddRange(Get(SelectChoise.True, SelectChoise.True));
        SecondaryList.Add(erAll);
        SecondaryList.AddRange(Get(SelectChoise.True, SelectChoise.False));
    }
    public List<EnergyResource> Get(SelectChoise SelectedActual, SelectChoise SelectedPrime)
    {
        this.SelectedActual = SelectedActual;
        this.SelectedPrime = SelectedPrime;
        return Get<EnergyResource>();
    }

    public List<T> Get<T>()
    {
        List<T> result = new();
        List<EnergyResource> list = new();

        string isActualStr = SelectedActual == SelectChoise.All ? "" : SelectedActual == SelectChoise.True ? " AND IsActual = 1" : " AND IsActual = 0";
        //string isMainStr = SelectedMain == SelectChoise.All ? "" : SelectedMain == SelectChoise.True ? " AND IsMain = 1" : " AND IsMain = 0"; ;
        string isPrimeStr = SelectedPrime == SelectChoise.All ? "" : SelectedPrime == SelectChoise.True ? " AND IsPrime = 1" : " AND IsPrime = 0";
        string whereStr = "WHERE True" + isActualStr + isPrimeStr;
        string sql = "SELECT Id, IdGroup, Name, ShortName, IdUnit, IsMain, IsActual, IsPrime FROM EnergyResources " + whereStr + " ORDER BY Id";

        DataTable dt = new DataTable();
        dt = Sqlite.Select(Global.dbpath, sql);
        list = (from DataRow dr in dt.Rows
                select new EnergyResource()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    IdGroup = Convert.ToInt32(dr["IdGroup"]),
                    Name = dr["Name"].ToString(),
                    NameFull = Convert.ToInt32(dr["Id"]) + "_" + dr["Name"].ToString(),
                    ShortName = dr["ShortName"].ToString(),
                    IdUnit = Convert.ToInt32(dr["IdUnit"]),
                    IsActual = Convert.ToBoolean(dr["IsActual"]),
                    IsMain = Convert.ToBoolean(dr["IsMain"]),
                    IsPrime = Convert.ToBoolean(dr["IsPrime"]),
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }
    public int Add(object rec)
    {
        if (rec == null) return -1;
        EnergyResource record = rec as EnergyResource;
        string sql = "INSERT INTO EnergyResources (Id, IdGroup, Name, ShortName, IdUnit, IsMain, IsActual, IsPrime) VALUES ("
                        + record.Id.ToString() + ", " + record.IdGroup.ToString() + ", '" + record.Name + "'" + ", '"
                        + record.ShortName + "'" + ", " + record.IdUnit.ToString() + ", " + record.IsMain.ToString() + ", "
                        + record.IsActual.ToString() + ", " + record.IsPrime.ToString() + ")";
        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }
    public int Delete(string where)
    {
        string sql = "Delete FROM EnergyResources WHERE " + where;
        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }
    public int Update(object rec)
    {
        if (rec == null) return -1;
        EnergyResource record = rec as EnergyResource;
        string sql = "UPDATE EnergyResources SET (IdGroup, Name, ShortName, IdUnit, IsMain, IsActual, IsPrime) = ( "
                        + record.IdGroup.ToString() + "'" + record.Name + "'" + ", '" + record.ShortName + "'" + ", "
                        + record.IdUnit.ToString() + ", " + record.IsMain.ToString() + ", "
                        + record.IsActual.ToString() + ", " + record.IsPrime.ToString() + ")"
                        + "WHERE Id = " + record.Id.ToString();

        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }

    #region INotifyProperty

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    protected bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(PropertyName);
        return true;
    }


    #endregion

}
