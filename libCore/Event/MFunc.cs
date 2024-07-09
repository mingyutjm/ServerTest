namespace LibCore
{

    public delegate TResult MFunc<out TResult>();
    public delegate TResult MFunc<in T, out TResult>(T t);
    public delegate TResult MFunc<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
    public delegate TResult MFunc<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);
    public delegate TResult MFunc<in T1, in T2, in T3, in T4, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    public delegate TResult MMultiFunc<out TResult>(params object[] args);

}