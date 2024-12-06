namespace WLib.System.ValueManagement
{
    public interface IProperty<TField> : IGetter<TField>, ISetter<TField>
    {
        TField Field { get; set; }
    }
}
