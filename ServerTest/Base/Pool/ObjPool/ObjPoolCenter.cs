namespace Server3
{

    public static class ObjPoolCenter
    {
        private static Dictionary<TypePair, IObjPool> s_pools =
            new Dictionary<TypePair, IObjPool>(TypePairCompare.Instance);

        // Obj with no argument
        public static ObjPool<TObj> Get<TObj>() where TObj : IPoolObj, new()
        {
            var id = TypePair.Create<TObj>();
            if (s_pools.TryGetValue(id, out var pool))
            {
                // use manual register
                return (pool as ObjPool<TObj>)!;
            }
            else
            {
                // Auto create
                pool = new ObjPool<TObj>();
                Register(pool);
                return (pool as ObjPool<TObj>)!;
            }
        }

        public static ObjPool<TObj, TArg> Get<TObj, TArg>() where TObj : IPoolObj<TArg>, new()
        {
            var id = TypePair.Create<TObj, TArg>();
            if (s_pools.TryGetValue(id, out var pool))
            {
                // use manual register
                return (pool as ObjPool<TObj, TArg>)!;
            }
            else
            {
                // Auto create
                pool = new ObjPool<TObj, TArg>();
                Register(pool);
                return (pool as ObjPool<TObj, TArg>)!;
            }
        }

        public static IObjPool? Get(in TypePair id)
        {
            return s_pools.GetValueOrDefault(id);
        }

        public static bool Register(IObjPool? pool)
        {
            if (pool == null)
                return false;

            if (!s_pools.TryAdd(pool.Id, pool))
            {
                Log.Error($"Pool{pool.Id} already exist!");
                return false;
            }
            return true;
        }

        public static void Unregister(IObjPool? pool)
        {
            if (pool == null)
                return;
            s_pools.Remove(pool.Id);
        }
    }

}