namespace FilterPanelTest.Models;

public class Unit : IDBModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double? K { get; set; }

    public List<T> Get<T>()
    {
        List<T> result = new();
        List<Unit> list = new();

        string sql = "SELECT Id, Name, K FROM Units ORDER BY Id";

        DataTable dt = new DataTable();
        dt = Sqlite.Select(sql);
        list = (from DataRow dr in dt.Rows
                select new Unit()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    K = Convert.ToDouble(dr["K"])
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }
    public int Add(object rec)
    {
        if (rec == null) return -1;
        Unit record = rec as Unit;
        string sql = "INSERT INTO Units (Id, Name, K) VALUES (" + record.Id.ToString() + ", '" + record.Name + "'" + ", '" + record.K + " )";
        return Sqlite.ExecNonQuery(sql);
    }
    public int Delete(string where)
    {
        string sql = "Delete FROM Units WHERE " + where;
        return Sqlite.ExecNonQuery(sql);
    }
    public int Update(object rec)
    {
        if (rec == null) return -1;
        Unit record = rec as Unit;
        string sql = "UPDATE Units SET (Name, K) = ( " + "'" + record.Name + "'" + ", '" + record.K + " )"
                        + "WHERE Id = " + record.Id.ToString();

        return Sqlite.ExecNonQuery(sql);
    }

}
