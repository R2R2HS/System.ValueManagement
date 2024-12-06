namespace WLib.System.ValueManagement
{
    public interface IObservableProperty<TField> : IProperty<TField>
    {
        event RefAction<TField, bool>? OnSet;
    }
}
