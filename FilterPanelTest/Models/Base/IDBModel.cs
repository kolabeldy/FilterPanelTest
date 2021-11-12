namespace FilterPanelTest.Models.Base;
public interface IDBModel
{
    List<T> Get<T>();
    int Add(object rec);
    int Delete(string where);
    int Update(object rec);
}
