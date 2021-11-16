namespace FilterPanelTest.FilterTree;
public class FilterSectionCostCentersViewModel : FilterSectionViewModel
{
    public override ObservableCollection<TreeFamily> RetTreeFamilies()
    {
        CostCenter cc = new CostCenter();
        List<CostCenter> _costCenters = new List<CostCenter>();

        ObservableCollection<TreeFamily> rez = new ObservableCollection<TreeFamily>();
        rez.Add(new TreeFamily()
        {
            Name = "Основные",
            Members = PList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.True, SelectedTechnology: SelectChoise.True))
        });
        rez.Add(new TreeFamily()
        {
            Name = "Прочие технологические",
            Members = PList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.False, SelectedTechnology: SelectChoise.True))
        });
        rez.Add(new TreeFamily()
        {
            Name = "Вспомогательные",
            Members = PList(cc.Get(SelectedActual: SelectChoise.All, SelectedMain: SelectChoise.False, SelectedTechnology: SelectChoise.False))
        });
        return rez;
    }
    protected List<TreePerson> PList(List<CostCenter> tList)
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
