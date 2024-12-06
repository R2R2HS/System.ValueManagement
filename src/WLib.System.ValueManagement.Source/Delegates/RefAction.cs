namespace WLib.System
{
    public delegate void RefAction<T>(ref T arg);

    public delegate void RefAction<T0, T1>(ref T0 arg0, ref T1 arg1);

    public delegate void RefAction<T0, T1, T2>(ref T0 arg0, ref T1 arg1, ref T2 arg2);

    public delegate void FirstRefAction<T0, T1>(ref T0 arg0, T1 arg1);
}
