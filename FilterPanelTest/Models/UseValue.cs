namespace FilterPanelTest.Models;

public class UseValue : NotifyBase
{
    private bool _IsFactChanged;
    private bool _IsPlanChanged;

    public long Id { get; set; }
    public Period Period { get; set; }
    public CostCenter CostCenter { get; set; }
    public int IdProduct { get; set; }
    public string ProductName { get; set; }
    public EnergyResource EnergyResource { get; set; }

    private double _Fact;
    public double Fact
    {
        get => _Fact;
        set
        {
            _IsFactChanged = true;
            _Fact = value;
            if (_IsPlanChanged)
                Diff = Fact - Plan;
            OnPropertyChanged("Fact");
        }
    }

    private double _Plan;
    public double Plan
    {
        get => _Plan;
        set
        {
            _IsPlanChanged = true;
            _Plan = value;
            if (_IsFactChanged)
                Diff = Fact - Plan;
            OnPropertyChanged("Plan");
        }
    }

    private double _Diff;
    public double Diff
    {
        get => _Diff;
        set
        {
            _Diff = value;
            _IsFactChanged = false;
            _IsPlanChanged = false;
            OnPropertyChanged("Diff");
        }
    }



}
