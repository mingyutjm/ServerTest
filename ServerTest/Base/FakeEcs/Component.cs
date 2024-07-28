namespace Server3.FakeEcs
{

    public abstract class Component : SnObject, IPoolObj
    {
        protected Entity _owner;

        public Entity Owner => _owner;

        public abstract void Dispose();

        public abstract void OnGet();

        public abstract void OnRelease();

        public void SetOwner(Entity owenr)
        {
            _owner = owenr;
        }
    }

}