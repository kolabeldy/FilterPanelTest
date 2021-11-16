namespace FilterPanelTest.Models;

public class Product : IdName, IDBModel
{
    public CostCenter CostCenter { get; set; }
    public int IdUnit { get; set; }
    public string UnitName { get; set; }


    //public List<Product> Get(int id)
    //{

    //}

    public List<T> Get<T>()
    {
        List<T> result = new();
        List<Product> list = new();

        string sql = "SELECT Id, Name, IdCC, IdUnit FROM Products ORDER BY Id";

        DataTable dt = new DataTable();
        dt = Sqlite.Select(Global.dbpath, sql);
        list = (from DataRow dr in dt.Rows
                select new Product()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    CostCenter = Global.CostCenterSourceList.Find(i => i.Id == Convert.ToInt32(dr["IdCC"])),
                    IdUnit = Convert.ToInt32(dr["IdUnit"]),
                    //UnitName = "-------"
                    UnitName = (Global.UnitSourceList.Find(i => i.Id == Convert.ToInt32(dr["IdUnit"]))).Name
                }).ToList();
        result.AddRange((IEnumerable<T>)list);
        return result;
    }

    public List<Product> GetFilterFromCC(List<CostCenter> cc)
    {
        var x = new HashSet<int>();
        foreach (CostCenter c in cc)
        {
            x.Add(c.Id);
        }
        List<Product> result = new();
        foreach (Product p in Get<Product>())
        {
            if (p.CostCenter != null)
            {
                Product pr = new();
                pr.Id = p.Id;
                pr.Name = p.Name;
                pr.CostCenter = p.CostCenter;
                pr.IdUnit = p.IdUnit;
                pr.UnitName = p.UnitName;
                if (x.Contains(pr.CostCenter.Id))
                {
                    result.Add(pr);
                }
            }
        }

        return result;
    }

    public int Add(object rec)
    {
        if (rec == null) return -1;
        else
        {
            Product? record = rec as Product;
            string sql = String.Format("INSERT INTO Products (Id, Name, IdCC, IdUnit) VALUES ( {0}, {1}, {2}, {3} )",
                                        record.Id.ToString(), record.Name, record.CostCenter.Id.ToString(), record.IdUnit.ToString());
            return Sqlite.ExecNonQuery(Global.dbpath, sql);
        }
    }
    public int Delete(string where)
    {
        string sql = "Delete FROM Products WHERE " + where;
        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }
    public int Update(object rec)
    {
        if (rec == null) return -1;
        Product? record = rec as Product;
        string sql = string.Format("UPDATE Products SET (Name, IdCC, IdUnit) = ({0}, {1}, {2}) WHERE Id = {3}",
                         record.Name, record.CostCenter.Id, record.IdUnit.ToString(), record.Id.ToString());

        return Sqlite.ExecNonQuery(Global.dbpath, sql);
    }
}
