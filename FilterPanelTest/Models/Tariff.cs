namespace FilterPanelTest.Models;

public class Tariff : IDBModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double? Value { get; set; }
    public int IdUnit { get; set; }

    public List<T> Get<T>()
    {
        List<T> result = new();
        List<Tariff> list = new();

        string sql = "SELECT Id, Name, Value, IdUnit FROM Tariffs ORDER BY Id";

        DataTable dt = new DataTable();
        dt = Sqlite.Select(Global.dbpath, sql);
        list = (from DataRow dr in dt.Rows
                select new Tariff()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Value = Convert.ToDouble(dr["Value"]),
                    IdUnit = Convert.ToInt32(dr["IdUnit"])
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }
    public int Add(object rec)
    {
        if (rec == null) return -1;
        Tariff record = rec as Tariff;
        string sql = "INSERT INTO Tariffs (Id, Name, Value, IdUnit) VALUES ("
            + record.Id.ToString() + ", '" + record.Name + "'" + ", '" + record.Value + ", '" + record.IdUnit + " )";
        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }
    public int Delete(string where)
    {
        string sql = "Delete FROM Tariffs WHERE " + where;
        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }
    public int Update(object rec)
    {
        if (rec == null) return -1;
        Tariff record = rec as Tariff;
        string sql = "UPDATE Tariffs SET (Name, Value, IdUnit) = ( " + "'" + record.Name + "'" + ", '" + record.Value + record.IdUnit + " )"
                        + "WHERE Id = " + record.Id.ToString();

        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }

}
