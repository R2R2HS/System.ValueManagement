namespace WLib.System.ValueManagement
{
    public class Property<TField>(TField value) : IProperty<TField>
    {
        private TField m_Field = value;

        public TField Field
        {
            get => Get();
            set => Set(value);
        }

        public TField Get() => m_Field;

        public void Set(TField value) => m_Field = value;
    }
}
