using System.Collections.Concurrent;

namespace Server3
{

    public static class TSObjPoolCenter
    {
        private static ConcurrentDictionary<TypePair, IObjPool> s_pools = new ConcurrentDictionary<TypePair, IObjPool>(TypePairCompare.Instance);
        private static object s_locker = new object();

        public static TSObjPool<TObj, TArg> Get<TObj, TArg>() where TObj : IPoolObj<TArg>, new()
        {
            var id = TypePair.Create<TObj, TArg>();
            lock (s_locker)
            {
                if (s_pools.TryGetValue(id, out var pool))
                {
                    // use manual register
                    return (pool as TSObjPool<TObj, TArg>)!;
                }
                else
                {
                    // Auto create
                    pool = new TSObjPool<TObj, TArg>();
                    s_pools.TryAdd(pool.Id, pool);
                    return (pool as TSObjPool<TObj, TArg>)!;
                }
            }
        }

        public static IObjPool? Get(in TypePair id)
        {
            lock (s_locker)
            {
                return s_pools.GetValueOrDefault(id);
            }
        }

        public static bool Register(IObjPool? pool)
        {
            if (pool == null)
                return false;

            lock (s_locker)
            {
                if (!s_pools.TryAdd(pool.Id, pool))
                {
                    Log.Error($"Pool{pool.Id} already exist!");
                    return false;
                }
                return true;
            }
        }

        public static void Unregister(IObjPool? pool)
        {
            if (pool == null)
                return;

            lock (s_locker)
            {
                s_pools.Remove(pool.Id, out _);
            }
        }
    }

}