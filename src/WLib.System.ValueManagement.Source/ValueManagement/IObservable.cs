using System;

namespace WLib.System.ValueManagement
{
    public interface IObservable
    {
        event Action? OnUpdate;
    }
}
