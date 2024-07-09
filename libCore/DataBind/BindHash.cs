using System.Collections;

namespace LibCore
{

    public interface IHashBind<TKey, TValue>
    {
        public void OnAdd(TKey key, TValue value);
        public void OnRemove(TKey key);
        public void OnChange(Dictionary<TKey, TValue> value);
        public void OnUpdate(TKey key, TValue value);
        public void OnClear();
    }

    /// NOTE: Use BindHashHelper class to bind hash
    public class BindHash<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IReference
    {
        private Dictionary<TKey, TValue> _values = new Dictionary<TKey, TValue>();

        private event MAction<TKey, TValue> OnAdd;
        private event MAction<TKey> OnRemove;
        private event MAction<TKey, TValue> OnUpdate;
        private event MAction OnClear;
        private event MAction<Dictionary<TKey, TValue>> OnChange;

        public int Count => _values.Count;

        public static BindHash<TKey, TValue> Create()
        {
            BindHash<TKey, TValue> hashMap = ReferencePool.Get<BindHash<TKey, TValue>>();
            return hashMap;
        }

        public void Dispose()
        {
            ReferencePool.Release(this);
        }

        #region Bind

        // Bind
        public void BindOnAdd(MAction<TKey, TValue> listener)
        {
            OnAdd += listener;
        }

        public void BindOnRemove(MAction<TKey> listener)
        {
            OnRemove += listener;
        }

        public void BindOnUpdate(MAction<TKey, TValue> listener)
        {
            OnUpdate += listener;
        }

        public void BindOnClear(MAction listener)
        {
            OnClear += listener;
        }

        public void BindOnChange(MAction<Dictionary<TKey, TValue>> listener)
        {
            OnChange += listener;
        }

        // Unbind
        public void UnbindOnAdd(MAction<TKey, TValue> listener)
        {
            OnAdd -= listener;
        }

        public void UnbindOnRemove(MAction<TKey> listener)
        {
            OnRemove -= listener;
        }

        public void UnbindOnUpdate(MAction<TKey, TValue> listener)
        {
            OnUpdate -= listener;
        }

        public void UnbindOnClear(MAction listener)
        {
            OnClear -= listener;
        }

        public void UnbindOnChange(MAction<Dictionary<TKey, TValue>> listener)
        {
            OnChange -= listener;
        }

        #endregion Bind

        public void Add(TKey key, TValue value)
        {
            if (_values.TryAdd(key, value))
            {
                OnAdd?.Invoke(key, value);
            }
        }

        public void Remove(TKey key)
        {
            if (_values.Remove(key))
            {
                OnRemove?.Invoke(key);
            }
        }

        public void Update(TKey key, TValue value)
        {
            if (!_values.ContainsKey(key))
            {
                Add(key, value);
                return;
            }

            _values[key] = value;
            OnUpdate?.Invoke(key, value);
        }

        public void Clear()
        {
            _values.Clear();
            OnClear?.Invoke();
        }

        public void Change(Dictionary<TKey, TValue> value)
        {
            _values = value;
            OnChange?.Invoke(value);
        }

        public bool ContainsKey(TKey key)
        {
            return _values.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return _values.ContainsValue(value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _values.TryGetValue(key, out value);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (_values.TryAdd(key, value))
            {
                OnAdd?.Invoke(key, value);
                return true;
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get => _values[key];
            set
            {
                if (_values.ContainsKey(key))
                {
                    Update(key, value);
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void IReference.Clear()
        {
            OnAdd = null;
            OnRemove = null;
            OnUpdate = null;
            OnClear = null;
            OnChange = null;
            _values.Clear();
        }
    }

}