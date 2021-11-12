namespace FilterPanelTest.Models;
public class NotifyBase : INotifyPropertyChanged
{
    private event PropertyChangedEventHandler? PropertyChangedHandlers;

    event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
    {
        add => PropertyChangedHandlers += value;
        remove => PropertyChangedHandlers -= value;
    }

    protected void OnPropertyChanged([CallerMemberName] string? PropertyName = null) => PropertyChangedHandlers?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

    protected bool Set<T>(ref T field, T value, [CallerMemberName] string? PropertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(PropertyName);
        return true;
    }


}
