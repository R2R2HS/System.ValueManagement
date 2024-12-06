using System.Collections.Generic;

namespace WLib.System.ValueManagement
{
    public interface IObservableArray<TElement> : IEnumerable<TElement>, IObservable
    {
        TElement this[int index] { get; set; }

        event RefAction<bool, TElement>? OnElementChange;
    }
}
