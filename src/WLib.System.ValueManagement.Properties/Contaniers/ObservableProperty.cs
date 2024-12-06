using System;
using System.Threading;

namespace WLib.System.ValueManagement
{
    public class ObservableProperty<TField>(IProperty<TField> source) : IObservableProperty<TField>, IObservable, ISource<IProperty<TField>>
    {
        private readonly
#if NET9_0_OR_GREATER
        Lock
#else
        object
#endif
        m_Sync = new();
        private readonly IProperty<TField> m_Source = source;

        public TField Field
        {
            get => Get();
            set => Set(value);
        }

        public IProperty<TField> Source => m_Source;

        public event RefAction<TField, bool>? OnSet;
        public event Action? OnUpdate;

        public TField Get() => m_Source.Get();

        public void Set(TField value)
        {
            lock (m_Sync)
            {
                bool shouldAllow = true;

                OnSet?.Invoke(ref value, ref shouldAllow);

                if (shouldAllow)
                {
                    m_Source.Set(value);
                    OnUpdate?.Invoke();
                }
            } 
        }
    }
}
