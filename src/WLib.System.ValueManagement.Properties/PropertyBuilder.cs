using System;

namespace WLib.System.ValueManagement
{
    public class PropertyBuilder
    {
        public static IProperty<TField> BuildProperty<TField>(TField value) => new Property<TField>(value);

        public static IProperty<TField> BuildProperty<TField>(Func<TField> getter, Action<TField> setter) => new PropertyTemplate<TField>(getter, setter);

        public static IGetter<TField> BuildReadOnlyProperty<TField>(IGetter<TField> getter) => new ReadOnlyProperty<TField>(getter);

        public static IObservableProperty<TField> BuildObservableProperty<TField>(IProperty<TField> property) => new ObservableProperty<TField>(property);

        public static IObservableProperty<TField> BuildObservableProperty<TField>(TField value) => BuildObservableProperty(BuildProperty(value));

        public static IObservableProperty<TField> BuildObservableProperty<TField>(Func<TField> getter, Action<TField> setter) => new ObservableProperty<TField>(BuildProperty(getter, setter));
    }
}
