using System.Collections.Generic;
using System.Windows;

namespace MyCheckedTreeLibrary;
public class TreeFamily : DependencyObject, IParent<object>
{
    public string Name { get; set; }
    public int Id { get; set; }
    public List<TreePerson>? Members { get; set; }

    IEnumerable<object>? IParent<object>.GetChildren()
    {
        return Members;
    }
}

public class TreePerson : DependencyObject
{
    public int Id { get; set; }
    public string? Name { get; set; }
}