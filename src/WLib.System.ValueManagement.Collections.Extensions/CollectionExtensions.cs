using System.Collections.Generic;

namespace WLib.System.ValueManagement
{
    public static class CollectionExtensions
    {
        public static IObservableArray<TElement> AsObservable<TElement>(this TElement[] array) => CollectionBuilder.BuildObservableArray(array);

        public static IObservableList<TElement> AsObservable<TElement>(this IList<TElement> list) => CollectionBuilder.BuildObservableList(list);

        public static IObservableDictionary<TKey, TElement> AsObservable<TKey, TElement>(this IDictionary<TKey, TElement> dictionary) => CollectionBuilder.BuildObservableDictionary(dictionary);
    }
}
