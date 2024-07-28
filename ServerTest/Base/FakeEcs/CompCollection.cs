namespace Server3.FakeEcs
{

    public class CompCollection
    {
        private List<Component> _comps;
        // 缓存通过Get拿到的Comp, 是否有必要?
        private SortedList<System.Type, Component> _cachedGetComps;
        public int Count => _comps.Count;

        public Component this[int index] => _comps[index];

        public CompCollection()
        {
            _comps = new List<Component>(8);
            _cachedGetComps = new SortedList<Type, Component>(5);
        }

        public bool Add(Component comp)
        {
            if (Contains(comp))
            {
                Log.Error($"The component <{comp}> has already been included, or a subclass or superclass of the component has been included.");
                return false;nnnnnnnnnnm
            }

            _comps.Add(comp);
            return true;
        }

        public bool Remove(Component comp)
        {
            _cachedGetComps.Remove(comp.GetType());
            return _comps.Remove(comp);
        }

        public TComp Get<TComp>() where TComp : Component
        {
            var queryType = typeof(TComp);

            // 命中缓存
            if (_cachedGetComps.TryGetValue(queryType, out Component cachedComp))
            {
                return cachedComp as TComp;
            }

            foreach (var comp in _comps)
            {
                var itType = comp.GetType();
                if (itType == queryType || itType.IsSubclassOf(queryType))
                {
                    _cachedGetComps.TryAdd(queryType, comp);
                    return comp as TComp;
                }
            }
            return null;
        }

        // 判断类型以及引用
        public bool Contains(Component comp)
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

        public bool Contains<TComp>() where TComp : Component
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
    }

}