namespace MyUserControlsLibrary.CaptionCard;

public partial class CaptionCardControl
{
    public CaptionCardViewModel Model { get; set; }
    public CaptionCardControl(string title = "Title", string nameContent = "Name Content")
    {
        Model = new(title, nameContent);
        InitializeComponent();
        DataContext = Model;
    }
}
