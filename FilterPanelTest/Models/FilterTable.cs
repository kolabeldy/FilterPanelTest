namespace FilterPanelTest.Models;

public class FilterTable
{
    public string Category { get; set; }
    public string Item { get; set; }
    public string Indicator { get; set; }
    public int Value { get; set; }

    private string selectedCategory;
    private string selectedItem;
    private string selectedIndicator;


    public List<FilterTable> Get(string category, string item, string indicator)
    {
        selectedCategory = category;
        selectedItem = item;
        selectedIndicator = indicator;
        return Get<FilterTable>();
    }

    public List<T> Get<T>()
    {
        List<T> result = new();
        List<FilterTable> list = new();

        string sql = string.Format("SELECT Category, Item, Indicator, Value FROM Filters WHERE Category = {0} and Item = {1} and Indicator = {2}",
                                        selectedCategory, selectedItem, selectedIndicator);

        //var resourceManager = Properties.Resources.ResourceManager;

        DataTable dt = new DataTable();
        dt = Sqlite.Select(Global.dbpath, sql);
        list = (from DataRow dr in dt.Rows
                select new FilterTable()
                {
                    Category = dr["Category"].ToString(),
                    Item = dr["Item"].ToString(),
                    Indicator = dr["Indicator"].ToString(),
                    Value = Convert.ToInt32(dr["Value"]),
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }

    public int Delete(string category, string item)
    {
        string sql = string.Format("Delete FROM Filters WHERE Category = '{0}' AND Item = '{1}'", category, item);
        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }
    public int AddRange(List<FilterTable> filterList)
    {
        List<string> listSQL = new();
        foreach (FilterTable record in filterList)
        {
            listSQL.Add(string.Format("INSERT INTO Filters (Category, Item, Indicator, Value) VALUES ('{0}', '{1}', '{2}', {3} )",
            record.Category.ToString(), record.Item.ToString(), record.Indicator, record.Value.ToString()));
        }
        return Sqlite.ExecNonQueryRange(Global.dbpath, listSQL);
    }
    public int Add(object rec)
    {
        if (rec == null) return -1;
        FilterTable record = rec as FilterTable;
        string sql = string.Format("INSERT INTO Filters (Category, Item, Indicator, Value) VALUES ('{0}', '{1}', '{2}', {3} )",
                        record.Category.ToString(), record.Item.ToString(), record.Indicator, record.Value.ToString());
        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }

}
