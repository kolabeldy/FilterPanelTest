namespace FilterPanelTest.Models;

public class Norm
{
    public int Id { get; set; }
    public int IdCostCenter { get; set; }
    public int IdProduct { get; set; }
    public int IdER { get; set; }
    public double K { get; set; }
    public double NormWinter { get; set; }
    public double NormSummer { get; set; }
    public string DateStart { get; set; }
    //public string? DateEnd { get; set; }
    public int IdProducer { get; set; }

    public List<Norm> source { get; set; }

    public List<T> Get<T>()
    {
        List<T> result = new();
        List<Norm> list = new();

        string sql = "SELECT Id, IdCostCenter, IdProduct, IdER, K, NormWinter, NormSummer, DateStart, DateEnd, IdProducer FROM Norms ORDER BY Id";

        DataTable dt = new DataTable();
        dt = Sqlite.Select(Global.dbpath, sql);
        list = (from DataRow dr in dt.Rows
                select new Norm()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    IdCostCenter = Convert.ToInt32(dr["IdCostCenter"]),
                    IdProduct = Convert.ToInt32(dr["IdProduct"]),
                    IdER = Convert.ToInt32(dr["IdER"]),
                    K = Convert.ToDouble(dr["K"]),
                    NormWinter = Convert.ToDouble(dr["NormWinter"]),
                    NormSummer = Convert.ToDouble(dr["NormSummer"]),
                    DateStart = dr["DateStart"].ToString(),
                    //DateEnd = dr["DateEnd"].ToString(),
                    IdProducer = Convert.ToInt32(dr["IdProducer"])
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }
    //public int Add()
    //{
    //    if (Record == null) return -1;
    //    string sql = "INSERT INTO Norms (IdCostCenter, IdProduct, IdER, K, NormWinter, NormSummer, DateStart, DateEnd, IdProducer) " 
    //        + String.Format("VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} )", 
    //        Record.IdCostCenter.ToString(), Record.IdProduct.ToString(), Record.IdER.ToString(), Record.K.ToString(),
    //        Record.NormWinter.ToString(), Record.NormSummer.ToString(), Record.DateStart.ToString(), Record.IdProducer.ToString());
    //    return Sqlite.ExecNonQuery(Global.dbpath, sql);
    //}
    //public int Delete(string where)
    //{
    //    string sql = "Delete FROM Norms WHERE " + where;
    //    return Sqlite.ExecNonQuery(Global.dbpath, sql);
    //}
    //public int Update()
    //{
    //    if (Record == null) return -1;
    //    string sql = "UPDATE Norms SET (IdCostCenter, IdProduct, IdER, K, NormWinter, NormSummer, DateStart, DateEnd, IdProducer) = "
    //        + String.Format("VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} )",
    //            Record.IdCostCenter.ToString(), Record.IdProduct.ToString(), Record.IdER.ToString(), Record.K.ToString(),
    //            Record.NormWinter.ToString(), Record.NormSummer.ToString(), Record.DateStart.ToString(), Record.IdProducer.ToString()) + " "
    //        + "WHERE Id = " + Record.Id.ToString();
    //    return Sqlite.ExecNonQuery(Global.dbpath, sql);
    //}



    //public static void DeleteAll()
    //{
    //    using (SqliteConnection db = new($"Filename={Global.dbpath}"))
    //    {
    //        db.Open();
    //        SqliteCommand deleteCommand = new();
    //        deleteCommand.Connection = db;
    //        deleteCommand.CommandText = "Delete FROM Norms";
    //        deleteCommand.ExecuteNonQuery();
    //        db.Close();
    //    }
    //}
    //public static void InsertRec(List<TempUseERTable> exportList)
    //{
    //    using (SqliteConnection db = new SqliteConnection($"Filename={Global.dbpath}"))
    //    {
    //        db.Open();
    //        string sqlText = default;
    //        string NormWinter = default;
    //        string NormSummer = default;
    //        //string DateEnd = default;
    //        string K = default;
    //        using (var transaction = db.BeginTransaction())
    //        {
    //            foreach (var r in exportList)
    //            {
    //                NormWinter = r.NormWinter != "" ? Convert.ToDouble(r.NormWinter).ToString().Replace(",", ".")
    //                    : r.NormAverage != "" ? Convert.ToDouble(r.NormAverage).ToString().Replace(",", ".")
    //                    : Convert.ToDouble(r.NormSummer).ToString().Replace(",", ".");
    //                NormSummer = r.NormSummer != "" ? Convert.ToDouble(r.NormSummer).ToString().Replace(",", ".")
    //                    : r.NormAverage != "" ? Convert.ToDouble(r.NormAverage).ToString().Replace(",", ".")
    //                    : Convert.ToDouble(r.NormWinter).ToString().Replace(",", ".");
    //                //DateEnd = r.DateEnd ?? "0";
    //                if (r.UnitName == "кВт.ч" || r.UnitName == "м3") K = "0.001";
    //                else K = "1";
    //                sqlText = string.Format("INSERT INTO Norms (IdCostCenter, IdProduct, IdER, NormWinter, NormSummer, DateStart, K, IdProducer) "
    //                    + "VALUES ({0},{1},{2},{3},{4},'{5}',{6},{7}) ", r.IdCC, r.IdProduct, r.IdER, NormWinter, NormSummer, r.DateStart, K, r.IdERProducer);

    //                var insertCmd = db.CreateCommand();
    //                insertCmd.CommandText = sqlText;
    //                insertCmd.ExecuteNonQuery();
    //            }
    //            transaction.Commit();
    //        }
    //        db.Close();
    //    }
    //}


}
