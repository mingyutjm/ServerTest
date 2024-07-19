using System.Collections;

namespace Server3
{

    /// <summary>
    /// ThreadSafe object pool
    /// </summary>
    public class TSObjPool<TObj, TArg> : IObjPool where TObj : IPoolObj<TArg>, new()
    {
        private object _objsLocker = new object();
        private object _cacheLocker = new object();

        private TypePair _id = new TypePair() { type1 = typeof(TObj), type2 = typeof(TArg) };
        private int _countAll;
        private int _maxCount = Int32.MaxValue;

        private CacheRefresh<TObj> _cache = CacheRefresh<TObj>.Create();
        private Stack<TObj> _objs = new Stack<TObj>(16);
        private List<TObj> _cacheRemovedObjs = new List<TObj>();

        public TypePair Id => _id;
        public int CountAll => _countAll;
        public int CountInActive => _objs.Count;
        public int CountActive => _countAll - _objs.Count;

        public TObj Get(TArg arg)
        {
            // Objs
            Monitor.Enter(_objsLocker);
            if (!_objs.TryPop(out var obj))
            {
                obj = new TObj();
                _countAll++;
            }
            Monitor.Exit(_objsLocker);
            obj.OnGet(arg);

            // Cache
            Monitor.Enter(_cacheLocker);
            _cache.AddCache.Add(obj);
            Monitor.Exit(_cacheLocker);
            return obj;
        }

        public void Release(TObj? obj)
        {
            if (obj == null)
                return;
            lock (_cacheLocker)
            {
                _cache.RemoveCache.Add(obj);
            }
        }

        public void Release(object? obj)
        {
            if (obj is TObj pObj)
                Release(pObj);
        }

        public void Tick(float deltaTime)
        {
            lock (_cacheLocker)
            {
                if (_cache.CanSwap())
                {
                    _cache.Swap(_cacheRemovedObjs);
                }
            }

            lock (_objsLocker)
            {
                foreach (var obj in _cacheRemovedObjs)
                {
                    _objs.Push(obj);
                }
            }
        }

        public void SetMaxSize(int size)
        {
            _maxCount = size;
        }

        public void Dispose()
        {
            foreach (var obj in _objs)
            {
                obj.Dispose();
            }
            _objs.Clear();
            _cache.Dispose();
        }
    }

}