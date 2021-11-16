namespace FilterPanelTest.FilterTree;
public class FilterSectionDateViewModel : FilterSectionViewModel
{
    public event IsFilterPanelClosed OnFilterPanelClosed;

    public override ObservableCollection<TreeFamily> RetTreeFamilies()
    {
        int periodFirst = Period.MinPeriod;
        int startYear = Period.MinYear;
        int lastPeriod = Period.MaxPeriod;
        int lastYear = Period.MaxYear;
        int lastMonth = Period.MaxMonth;
        int[] arrYear = new int[lastYear - startYear + 1];
        for (int i = 0; i < (lastYear - startYear + 1); i++)
        {
            arrYear[i] = startYear + i;
        }
        ObservableCollection<TreeFamily> resultFamilies = new ObservableCollection<TreeFamily>();
        for (int y = startYear; y < lastYear; y++)
        {
            resultFamilies.Add(new TreeFamily()
            {
                Name = y.ToString(),
                Members = PList1(y)
            });
        }
        resultFamilies.Add(new TreeFamily()
        {
            Name = lastYear.ToString(),
            Members = PList2(lastYear)
        });
        return resultFamilies;

        List<TreePerson> PList1(int year)
        {
            List<TreePerson> resultPersons = new List<TreePerson>();
            for (int m = 1; m <= 12; m++)
            {
                resultPersons.Add(new TreePerson()
                {
                    Id = year * 100 + m,
                    Name = year.ToString() + " " + Period.monthArray[m - 1]
                });
            }
            return resultPersons;
        }
        List<TreePerson> PList2(int year)
        {
            List<TreePerson> rez2 = new List<TreePerson>();
            for (int m = 1; m <= lastMonth; m++)
            {
                rez2.Add(new TreePerson()
                {
                    Id = year * 100 + m,
                    Name = year.ToString() + " " + Period.monthArray[m - 1]
                });
            }
            return rez2;
        }
    }

}
