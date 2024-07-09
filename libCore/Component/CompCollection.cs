using System.Collections;

namespace LibCore
{

    public class CompCollection : IEnumerable<IComp>, IReference
    {
        private List<IComp> _comps;
        // 缓存通过Get拿到的Comp, 是否有必要?
        private SortedList<Type, IComp> _cachedGetComps;
        public int Count => _comps.Count;

        public IComp this[int index] => _comps[index];

        public static CompCollection Create()
        {
            return ReferencePool.Get<CompCollection>();
        }

        public CompCollection()
        {
            _comps = new List<IComp>(8);
            _cachedGetComps = new SortedList<Type, IComp>(5);
        }

        public bool Add(IComp comp)
        {
            if (Contains(comp))
            {
                Log.Error($"The component <{comp}> has already been included, or a subclass or superclass of the component has been included.");
                return false;
            }

            int index = _comps.FindIndex(it => it.UpdateOrder > comp.UpdateOrder);
            if (index >= 0)
                _comps.Insert(index, comp);
            else
                _comps.Add(comp);
            return true;
        }

        public bool Remove(IComp comp)
        {
            _cachedGetComps.Remove(comp.GetType());
            return _comps.Remove(comp);
        }

        public TComponent Get<TComponent>() where TComponent : class, IComp
        {
            var queryType = typeof(TComponent);

            // 命中缓存
            if (_cachedGetComps.TryGetValue(queryType, out IComp cachedComp))
            {
                return cachedComp as TComponent;
            }

            foreach (var comp in _comps)
            {
                var itType = comp.GetType();
                if (itType == queryType || itType.IsSubclassOf(queryType))
                {
                    _cachedGetComps.TryAdd(queryType, comp);
                    return comp as TComponent;
                }
            }
            return null;
        }

        public void UpdateComps(float deltaTime)
        {
            for (int i = 0; i < _comps.Count; i++)
            {
                try
                {
                    _comps[i].Update(deltaTime);
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            }
        }

        public void FixedUpdateComps()
        {
            for (int i = 0; i < _comps.Count; i++)
            {
                try
                {
                    _comps[i].FixedUpdate();
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            }
        }

        public void LateUpdateComps()
        {
            for (int i = 0; i < _comps.Count; i++)
            {
                try
                {
                    _comps[i].LateUpdate();
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            }
        }
        
        public void LateFixedUpdateComps()
        {
            for (int i = 0; i < _comps.Count; i++)
            {
                try
                {
                    _comps[i].LateFixedUpdate();
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            }
        }

        // 判断类型以及引用
        public bool Contains(IComp comp)
        {
            var queryType = comp.GetType();
            for (int i = 0; i < _comps.Count; i++)
            {
                // 判断引用
                if (comp == _comps[i])
                    return true;

                // 判断类型，包括子类
                var itCompType = _comps[i].GetType();
                if (queryType == itCompType || queryType.IsSubclassOf(itCompType) || itCompType.IsSubclassOf(queryType))
                    return true;
            }
            return false;
        }

        public bool Contains<TComp>() where TComp : class, IComp
        {
            var queryType = typeof(TComp);
            for (int i = 0; i < _comps.Count; i++)
            {
                // 判断类型，包括子类
                var itCompType = _comps[i].GetType();
                if (queryType == itCompType || queryType.IsSubclassOf(itCompType) || itCompType.IsSubclassOf(queryType))
                    return true;
            }
            return false;
        }
        
        public IEnumerator<IComp> GetEnumerator()
        {
            for (int i = 0; i < _comps.Count; i++)
                yield return _comps[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IComp>)this).GetEnumerator();
        }

        public void Dispose()
        {
            for (int i = _comps.Count - 1; i >= 0; i--)
            {
                _comps[i].Dispose();
            }
            // ReferencePool.Release(this);

            _cachedGetComps.Clear();
            _comps.Clear();
        }

        void IReference.Clear()
        {
            _cachedGetComps.Clear();
            _comps.Clear();
        }
    }

}