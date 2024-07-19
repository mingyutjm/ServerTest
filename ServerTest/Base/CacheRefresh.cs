namespace Server3
{

    public class CacheRefresh<T> : IReference where T : IReference
    {
        protected List<T> _read;
        protected List<T> _add;
        protected List<T> _remove;

        public List<T> ReadCache => _read;
        public List<T> AddCache => _add;
        public List<T> RemoveCache => _remove;

        public static CacheRefresh<T> Create()
        {
            CacheRefresh<T> cr = new CacheRefresh<T>();
            return cr;
        }

        private CacheRefresh()
        {
            _read = new List<T>();
            _add = new List<T>();
            _remove = new List<T>();
        }

        public void Swap(List<T>? removeList)
        {
            foreach (var toAdd in _add)
            {
                _read.Add(toAdd);
            }
            _add.Clear();

            removeList?.Clear();
            foreach (var toRemove in _remove)
            {
                int findIndex = _read.IndexOf(toRemove);
                if (findIndex != -1)
                {
                    _read.RemoveAt(findIndex);
                    removeList?.Add(toRemove);
                }
            }
            _remove.Clear();
        }

        public bool CanSwap()
        {
            return _add.Count > 0 || _remove.Count > 0;
        }

        public void Dispose()
        {
            foreach (var it in _read)
            {
                it.Dispose();
            }
            _read.Clear();
            foreach (var it in _add)
            {
                it.Dispose();
            }
            _add.Clear();
            foreach (var it in _remove)
            {
                it.Dispose();
            }
            _remove.Clear();
        }
    }

}