using System.Collections.Generic;

namespace MyCheckedTreeLibrary;
interface IParent<T>
{
    IEnumerable<T> GetChildren();
}