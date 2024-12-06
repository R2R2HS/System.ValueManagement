namespace WLib.System.ValueManagement
{
    public interface ISource<TSource>
    {
        TSource Source { get; }
    }
}
