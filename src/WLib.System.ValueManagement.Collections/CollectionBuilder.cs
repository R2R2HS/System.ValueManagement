using System.Collections.Generic;

namespace WLib.System.ValueManagement
{
    public class CollectionBuilder
    {
        public static IObservableArray<TElement> BuildObservableArray<TElement>(TElement[] array) => new ObservableArray<TElement>(array);

        public static IObservableList<TElement> BuildObservableList<TElement>(IList<TElement> list) => new ObservableList<TElement>(list);

        public static IObservableDictionary<TKey, TElement> BuildObservableDictionary<TKey, TElement>(IDictionary<TKey, TElement> dictionary) => new ObservableDictionary<TKey, TElement>(dictionary);
    }
}
