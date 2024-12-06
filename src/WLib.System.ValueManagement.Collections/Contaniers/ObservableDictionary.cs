using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace WLib.System.ValueManagement
{
    public class ObservableDictionary<TKey, TElement>(IDictionary<TKey, TElement> source) : IObservableDictionary<TKey, TElement>, ISource<IDictionary<TKey, TElement>>
    {
        private readonly
#if NET9_0_OR_GREATER
        Lock
#else
        object
#endif
        m_Sync = new();
        private readonly IDictionary<TKey, TElement> m_Source = source;

        public TElement this[TKey key]
        {
            get => m_Source[key];
            set
            {
                lock (m_Sync)
                {
                    bool shouldSet = true;
                    TElement setValue = value;
                    OnElementChange?.Invoke(ref shouldSet, ref key, ref setValue);

                    if (shouldSet)
                    {
                        m_Source[key] = setValue;
                        OnUpdate?.Invoke();
                    }
                } 
            }
        }

        public IDictionary<TKey, TElement> Source => m_Source;

        public bool IsReadOnly => m_Source.IsReadOnly;

        public int Count => m_Source.Count;

        public ICollection<TKey> Keys => m_Source.Keys;

        public ICollection<TElement> Values => m_Source.Values;

        public event RefAction<bool, TKey, TElement>? OnElementChange;
        public event RefAction<bool, TKey, TElement>? OnKeyAdd;
        public event FirstRefAction<bool, TKey>? OnKeyRemove;
        public event RefAction<bool, KeyValuePair<TKey, TElement>>? OnAdd;
        public event FirstRefAction<bool, KeyValuePair<TKey, TElement>>? OnRemove;
        public event Action? OnClear;
        public event Action? OnUpdate;

        public bool Contains(KeyValuePair<TKey, TElement> item) => m_Source.Contains(item);

        public bool ContainsKey(TKey key) => m_Source.ContainsKey(key);

        public bool TryGetValue(TKey key, out TElement value) => m_Source.TryGetValue(key, out value!);

        public void Add(TKey key, TElement value)
        {
            lock (m_Sync)
            {
                bool shouldAdd = true;
                OnKeyAdd?.Invoke(ref shouldAdd, ref key, ref value);

                if (shouldAdd)
                {
                    m_Source.Add(key, value);
                    OnUpdate?.Invoke();
                }
            }   
        }

        public void Add(KeyValuePair<TKey, TElement> item)
        {
            lock (m_Sync)
            {
                bool shouldAdd = true;
                OnAdd?.Invoke(ref shouldAdd, ref item);

                if (shouldAdd)
                {
                    m_Source.Add(item);
                    OnUpdate?.Invoke();
                }
            }              
        }

        public bool Remove(TKey key)
        {
            lock (m_Sync)
            {
                bool shouldRemove = true;
                OnKeyRemove?.Invoke(ref shouldRemove, key);

                if (shouldRemove)
                {
                    shouldRemove = m_Source.Remove(key);
                    OnUpdate?.Invoke();
                }

                return shouldRemove;
            }
        }

        public bool Remove(KeyValuePair<TKey, TElement> item)
        {
            lock (m_Sync)
            {
                bool shouldRemove = true;
                OnRemove?.Invoke(ref shouldRemove, item);

                if (shouldRemove)
                {
                    shouldRemove = m_Source.Remove(item);
                    OnUpdate?.Invoke();
                }

                return shouldRemove;
            }    
        }

        public void Clear()
        {
            lock (m_Sync)
            {
                m_Source.Clear();
                OnClear?.Invoke();
                OnUpdate?.Invoke();
            }
        }

        public void CopyTo(KeyValuePair<TKey, TElement>[] array, int arrayIndex) => m_Source.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TKey, TElement>> GetEnumerator() => m_Source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => m_Source.GetEnumerator();
    }
}
