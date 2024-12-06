using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace WLib.System.ValueManagement
{
    public class ObservableList<TElement>(IList<TElement> source) : IObservableList<TElement>, ISource<IList<TElement>>
    {
        private readonly
#if NET9_0_OR_GREATER
        Lock
#else
        object
#endif
        m_Sync = new();
        private readonly IList<TElement> m_Source = source;

        public TElement this[int index]
        {
            get => m_Source[index];
            set
            {
                lock (m_Sync)
                {
                    bool shouldSet = true;
                    TElement setValue = value;

                    OnElementChange?.Invoke(ref shouldSet, ref setValue);

                    if (shouldSet)
                    {
                        m_Source[index] = setValue;
                        OnUpdate?.Invoke();
                    }
                }
            }
        }

        public IList<TElement> Source => m_Source;

        public bool IsReadOnly => m_Source.IsReadOnly;

        public int Count => m_Source.Count;

        public event RefAction<bool, TElement>? OnElementChange;
        public event RefAction<bool, TElement>? OnAdd;
        public event FirstRefAction<bool, TElement>? OnRemove;
        public event RefAction<bool, int, TElement>? OnElementInsert;
        public event Action? OnClear;
        public event Action? OnUpdate;

        public bool Contains(TElement item) => m_Source.Contains(item);

        public int IndexOf(TElement item) => m_Source.IndexOf(item);

        public void Add(TElement item)
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

        public bool Remove(TElement item)
        {
            lock (m_Sync)
            {
                int index = IndexOf(item);
                if (index < 0 || index >= m_Source.Count) return false;
                RemoveAt(index);
                return true;
            }   
        }

        public void RemoveAt(int index)
        {
            lock (m_Sync)
            {
                bool shouldRemove = true;
                OnRemove?.Invoke(ref shouldRemove, m_Source[index]);

                if (shouldRemove)
                {
                    m_Source.RemoveAt(index);
                    OnUpdate?.Invoke();
                }
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

        public void Insert(int index, TElement item)
        {
            lock (m_Sync)
            {
                bool shouldInsert = true;
                OnElementInsert?.Invoke(ref shouldInsert, ref index, ref item);

                if (shouldInsert)
                {
                    m_Source.Insert(index, item);
                    OnUpdate?.Invoke();
                }
            }   
        }

        public void CopyTo(TElement[] array, int arrayIndex) => m_Source.CopyTo(array, arrayIndex);

        public IEnumerator<TElement> GetEnumerator() => m_Source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
