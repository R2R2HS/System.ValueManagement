using System.Collections.Generic;

namespace WLib.System.ValueManagement
{
    public interface IObservableDictionary<TKey, TElement> : IDictionary<TKey, TElement>, IObservableCollection<KeyValuePair<TKey, TElement>>
    {
        event RefAction<bool, TKey, TElement>? OnKeyAdd;

        event FirstRefAction<bool, TKey>? OnKeyRemove;

        event RefAction<bool, TKey, TElement>? OnElementChange;
    }
}
