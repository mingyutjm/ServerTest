namespace Server3.FakeEcs
{

    public class Entity : IPoolObj
    {
        // private Dictionary<Type, Component> _comps = new Dictionary<Type, Component>();
        private List<Component> _comps = new List<Component>();

        public void Dispose()
        {
        }

        public void OnGet()
        {
        }

        public void OnRelease()
        {
        }

        public T AddComp<T>() where T : Component, new()
        {
            var comp = Obj.Create<T>();
            _comps.Add(comp.GetType(), comp);
            return comp;
        }

        public T GetComp<T>() where T : Component
        {
            if (_comps.TryGetValue(typeof(T), out var comp))
            {
                return comp as T;
            }
            return null;
        }

        public bool ContainsComp<T>()
        {
        }
    }

}