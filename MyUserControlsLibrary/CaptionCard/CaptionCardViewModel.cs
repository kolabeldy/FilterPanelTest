using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyUserControlsLibrary.CaptionCard;

public partial class CaptionCardViewModel : INotifyPropertyChanged
{
    public string Title { get; set; }

    private string _NameContent;
    public string NameContent
    {
        get => _NameContent;
        set
        {
            Set(ref _NameContent, value);
        }
    }


    public CaptionCardViewModel(string title, string nameContent)
    {
        Title = title;
        NameContent = nameContent;
    }

    #region INotifyProperty

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    protected bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(PropertyName);
        return true;
    }
    #endregion

}
