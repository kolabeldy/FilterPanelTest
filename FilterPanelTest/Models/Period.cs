namespace FilterPanelTest.Models;

public enum MonthOutputStyle { AsNumeric, AsString };
public class Period : INotifyPropertyChanged, IDBModel
{
    private int _Id;
    public int Id
    {
        get => _Id;
        set
        {
            Name = GetPeriodName(value);
            NameFull = GetPeriodName(value, MonthOutputStyle.AsString);
            Year = GetYear(value);
            Month = GetMonth(value);
            MonthName = GetMonthName(Month);
            Set(ref _Id, value);
        }
    }

    private string _Name;
    public string Name { get => _Name; set => Set(ref _Name, value); }

    private string _NameFull;
    public string NameFull { get => _NameFull; set => Set(ref _NameFull, value); }

    private int _Year;
    public int Year { get => _Year; set => Set(ref _Year, value); }

    private int _Month;
    public int Month { get => _Month; set => Set(ref _Month, value); }

    private string _MonthName;
    public string MonthName { get => _MonthName; set => Set(ref _MonthName, value); }

    private DateTime _SelectedStartDate;
    public DateTime SelectedStartDate
    {
        get => _SelectedStartDate;
        set
        {
            SelectedStartPeriod = DateTimeToInt(value);
            Set(ref _SelectedStartDate, value);
        }
    }

    private DateTime _SelectedEndDate;
    public DateTime SelectedEndDate
    {
        get => _SelectedEndDate;
        set
        {
            SelectedEndPeriod = DateTimeToInt(value);
            Set(ref _SelectedEndDate, value);
        }
    }

    private int _SelectedStartPeriod;
    public int SelectedStartPeriod { get => _SelectedStartPeriod; set => Set(ref _SelectedStartPeriod, value); }

    private int _SelectedEndPeriod;
    public int SelectedEndPeriod { get => _SelectedEndPeriod; set => Set(ref _SelectedEndPeriod, value); }

    public static int MinPeriod { get; set; }
    public static int MaxPeriod { get; set; }
    public static int MinYear { get; set; }
    public static int MaxYear { get; set; }
    public static int MinMonth { get; set; }
    public static int MaxMonth { get; set; }
    public int MinSelectedPeriod { get; set; }
    public int MaxSelectedPeriod { get; set; }
    public int MinDynamicSelectedPeriod { get; set; }
    public int MaxDynamicSelectedPeriod { get; set; }
    public List<Period> Periods { get; set; }

    public Period()
    {
    }
    public static int GetYear(int period) => period / 100;
    public static int GetMonth(int period) => period - GetYear(period) * 100;
    public static string GetMonthName(int month) => monthArray[month - 1];
    public void SetDynamicPeriods()
    {
        int monthCount = DifferenceBetweenDatesInMonth(SelectedStartPeriod, SelectedEndPeriod);
        if (monthCount > 2)
        {
            MinDynamicSelectedPeriod = DateTimeToInt(SelectedStartDate);
            MaxDynamicSelectedPeriod = DateTimeToInt(SelectedEndDate);
        }
        else
        {
            int monthCountFromMax = DifferenceBetweenDatesInMonth(SelectedEndPeriod, MaxPeriod);
            if (monthCountFromMax > Global.DynamicPeriodMonthCount / 2 - 1)
            {
                MaxDynamicSelectedPeriod = PeriodMonthAdd(SelectedEndPeriod, Global.DynamicPeriodMonthCount / 2 - 1);
                MinDynamicSelectedPeriod = PeriodMonthAdd(MaxDynamicSelectedPeriod, -Global.DynamicPeriodMonthCount + 1);
            }
            else
            {
                MaxDynamicSelectedPeriod = MaxPeriod;
                MinDynamicSelectedPeriod = PeriodMonthAdd(MaxDynamicSelectedPeriod, -Global.DynamicPeriodMonthCount + 1);
            }
        }
    }
    public void Init((int minPeriod, int maxPeriod) periods)
    {
        MinPeriod = periods.minPeriod;
        MaxPeriod = periods.maxPeriod;
        MinYear = GetYear(MinPeriod);
        MaxYear = GetYear(MaxPeriod); ;
        MinMonth = GetMonth(MinPeriod);
        MaxMonth = GetMonth(MaxPeriod);
    }

    private static int DateTimeToInt(DateTime date)
    {
        return date.Year * 100 + date.Month;
    }
    private static string GetPeriodName(int period, MonthOutputStyle monthStyle = MonthOutputStyle.AsNumeric)
    {
        int year = GetYear(period);
        int month = period - year * 100;
        if (monthStyle == MonthOutputStyle.AsNumeric)
            return year + "_" + month;
        else return year + " " + monthArray[month - 1];
    }

    private static int PeriodMonthAdd(DateTime period, int month)
    {
        return DateTimeToInt(period.AddMonths(month));

    }
    private static int PeriodMonthAdd(int period, int month)
    {
        DateTime date = new DateTime(GetYear(period), GetMonth(period), 1);
        return DateTimeToInt(date.AddMonths(month));

    }

    private static int DifferenceBetweenDatesInMonth(DateTime datestart, DateTime dateend)
    {
        return ((dateend.Year - datestart.Year) * 12) + dateend.Month - datestart.Month;
    }
    private static int DifferenceBetweenDatesInMonth(int datestart, int dateend)
    {
        return ((GetYear(dateend) - GetYear(datestart)) * 12) + GetMonth(dateend) - GetMonth(datestart);
    }

    private static string[] monthArray = new string[]
    { "янв", "фев", "мар", "апр", "май", "июн", "июл", "авг", "сен", "окт", "ноя", "дек" };

    #region IDBModel realisattion

    public List<T> Get<T>()
    {
        //List<T> result = new();
        //List<Period> list = new();

        //var resourceManager = Properties.Resources.ResourceManager;
        //string sql = "SELECT MIN(Period) FROM ERUsesMonth GROUP By Period";

        //DataTable dt = new DataTable();
        //dt = Sqlite.Select(sql);
        //list = (from DataRow dr in dt.Rows
        //        select new CostCenter()
        //        {
        //            Id = Convert.ToInt32(dr["Id"]),
        //            Name = dr["Name"].ToString(),
        //            IsActual = Convert.ToBoolean(dr["IsActual"]),
        //            IsMain = Convert.ToBoolean(dr["IsMain"]),
        //            IsTechnology = Convert.ToBoolean(dr["IsTechnology"]),
        //        }).ToList();
        //result.AddRange((IEnumerable<T>)list);
        //return result;
        return null;
    }
    public int Add(object rec)
    {
        //if (rec == null) return -1;
        //CostCenter record = rec as CostCenter;
        //string sql = "INSERT INTO CostCenters (Id, Name, IsMain, IsTechnology, IsActual) VALUES ("
        //                + record.Id.ToString() + ", '" + record.Name + "'" + ", " + record.IsMain.ToString() + ", "
        //                + record.IsTechnology.ToString() + ", " + record.IsActual.ToString() + ")";
        //return Sqlite.ExecNonQuery(sql);
        return 0;
    }
    public int Delete(string whereStr)
    {
        //string sql = "Delete FROM CostCenters WHERE " + whereStr;
        //return Sqlite.ExecNonQuery(sql);
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

        //return Sqlite.ExecNonQuery(sql);
        return 0;
    }

    #endregion

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
