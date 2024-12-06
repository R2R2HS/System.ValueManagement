using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace WLib.System.ValueManagement
{
    public class ObservableArray<TElement>(params TElement[] source) : IObservableArray<TElement>, ISource<TElement[]>
    {
        private readonly
#if NET9_0_OR_GREATER
        Lock
#else
        object
#endif
        m_Sync = new();
        private readonly TElement[] m_Source = source;

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

        public TElement[] Source => m_Source;

        public event RefAction<bool, TElement>? OnElementChange;
        public event Action? OnUpdate;

        public IEnumerator<TElement> GetEnumerator() => ((IEnumerable<TElement>)m_Source).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => m_Source.GetEnumerator();
    }
}
