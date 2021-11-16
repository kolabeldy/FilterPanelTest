namespace FilterPanelTest.FilterTree;
public class FilterSectionEnergyResourcesViewModel : FilterSectionViewModel
{
    public override ObservableCollection<TreeFamily> RetTreeFamilies()
    {
        EnergyResource er = new EnergyResource();
        List<EnergyResource> _costCenters = new List<EnergyResource>();

        ObservableCollection<TreeFamily> rez = new ObservableCollection<TreeFamily>();
        rez.Add(new TreeFamily()
        {
            Name = "Первичные",
            Members = PList(er.Get(SelectedActual: SelectChoise.All, SelectedPrime: SelectChoise.True))
        });
        rez.Add(new TreeFamily()
        {
            Name = "Вторичные",
            Members = PList(er.Get(SelectedActual: SelectChoise.All, SelectedPrime: SelectChoise.False))
        });
        return rez;
    }
    protected List<TreePerson> PList(List<EnergyResource> tList)
    {
        List<TreePerson> rez1 = new List<TreePerson>();
        foreach (var r in tList)
        {
            TreePerson n = new TreePerson();
            n.Id = r.Id;
            n.Name = r.Name;
            rez1.Add(n);
        }
        return rez1;
    }

}
