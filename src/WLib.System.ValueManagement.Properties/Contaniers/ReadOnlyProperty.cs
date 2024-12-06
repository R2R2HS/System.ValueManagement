namespace WLib.System.ValueManagement
{
    public class ReadOnlyProperty<TField>(IGetter<TField> source) : IGetter<TField>, ISource<IGetter<TField>>
    {
        private readonly IGetter<TField> m_Source = source;

        public IGetter<TField> Source => m_Source;

        public TField Get() => Source.Get();
    }
}
