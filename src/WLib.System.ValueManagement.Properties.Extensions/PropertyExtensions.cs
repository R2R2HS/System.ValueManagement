namespace WLib.System.ValueManagement
{
    public static class PropertyExtensions
    {
        public static IObservableProperty<TField> AsObservable<TField>(this IProperty<TField> property) => PropertyBuilder.BuildObservableProperty(property);

        public static IGetter<TField> AsReadOnly<TField>(this IGetter<TField> getter) => PropertyBuilder.BuildReadOnlyProperty(getter);

        public static IProperty<TField> AsProperty<TField>(this TField value) => PropertyBuilder.BuildProperty(value);
    }
}
