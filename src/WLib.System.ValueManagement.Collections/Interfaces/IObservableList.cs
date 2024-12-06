using System.Collections.Generic;

namespace WLib.System.ValueManagement
{
    public interface IObservableList<TElement> : IList<TElement>, IObservableArray<TElement>, IObservableCollection<TElement>
    {
        event RefAction<bool, int, TElement>? OnElementInsert;
    }
}