using System;

namespace WLib.System.ValueManagement
{
    public readonly struct PropertyTemplate<TField>(Func<TField> getter, Action<TField> setter) : IProperty<TField>
    {
        private readonly Func<TField> m_Getter = getter;
        private readonly Action<TField> m_Setter = setter;

        public readonly TField Field
        {
            get => Get();
            set => Set(value);
        }

        public readonly TField Get() => m_Getter.Invoke();

        public readonly void Set(TField value) => m_Setter.Invoke(value);
    }
}
