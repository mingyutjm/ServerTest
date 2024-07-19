namespace Server3
{

    public class CacheSwap<T> : IReference where T : IReference
    {
        private List<T>? _readCache;
        private List<T>? _writeCache;

        public List<T> ReadCache => _readCache!;
        public List<T> WriteCache => _writeCache!;

        public static CacheSwap<T> Create()
        {
            CacheSwap<T> sp = new CacheSwap<T>();
            return sp;
        }

        private CacheSwap()
        {
            _readCache = new List<T>();
            _writeCache = new List<T>();
        }

        public void Swap()
        {
            (_readCache, _writeCache) = (_writeCache, _readCache);
        }

        public bool CanSwap()
        {
            return _writeCache.Count > 0;
        }

        public void Dispose()
        {
            foreach (var it in _readCache)
            {
                it.Dispose();
            }
            _readCache.Clear();
            foreach (var it in _writeCache)
            {
                it.Dispose();
            }
            _writeCache.Clear();
        }
    }

}