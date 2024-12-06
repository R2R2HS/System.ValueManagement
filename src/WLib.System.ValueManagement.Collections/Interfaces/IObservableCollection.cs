using System;
using System.Collections.Generic;

namespace WLib.System.ValueManagement
{
    public interface IObservableCollection<TElement> : ICollection<TElement>, IObservable
    {
        event RefAction<bool, TElement>? OnAdd;

        event FirstRefAction<bool, TElement>? OnRemove;

        event Action? OnClear;
    }
}
