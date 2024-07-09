namespace LibCore
{

    public delegate void MAction();
    public delegate void MAction<in T>(T t);
    public delegate void MAction<in T1, in T2>(T1 arg1, T2 arg2);
    public delegate void MAction<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3);
    public delegate void MAction<in T1, in T2, in T3, in T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    public delegate void MMultiAction(params object[] args);

}