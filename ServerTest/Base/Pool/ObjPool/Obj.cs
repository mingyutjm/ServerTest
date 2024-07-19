namespace Server3
{

    public static class Obj
    {
        public static TObj Create<TObj, TArg>(TArg arg) where TObj : IPoolObj<TArg>, new()
        {
            ObjPool<TObj, TArg> pool = ObjPoolCenter.Get<TObj, TArg>();
            return pool.Get(arg);
        }

        /// <summary>
        /// class type use
        /// </summary>
        /// <param name="obj">IPoolObj</param>
        /// <returns>Get pool success or fail</returns>
        public static bool Destroy<TArg>(IPoolObj<TArg>? obj)
        {
            if (obj == null)
                return false;
            IObjPool? pool = ObjPoolCenter.Get(TypePair.Create(obj.GetType(), typeof(TArg)));
            if (pool == null)
                return false;
            pool.Release(obj);
            return true;
        }

        /// value type use, to avoid boxing
        public static void DestroyStruct<TObj, TArg>(TObj? obj) where TObj : IPoolObj<TArg>, new()
        {
            if (obj == null)
                return;
            ObjPool<TObj, TArg> pool = ObjPoolCenter.Get<TObj, TArg>();
            pool.Release(obj);
        }
    }

}