namespace LibCore
{

    public abstract class Comp : IComp, ICompOwner
    {
        protected bool _enabled;
        protected bool _disposed;
        protected ICompOwner _owner;

        protected CompCollection _comps;
        public CompCollection Comps => _comps;

        public ICompOwner Owner => _owner;

        public bool IsEnabled => _enabled;
        public bool IsDisposed => _disposed;

        public virtual int UpdateOrder => 100;
        public string CompTypeName { get; private set; }

        public Comp()
        {
            _disposed = false;
            OnCreate();
            CompTypeName = GetType().Name;
        }

        public void Dispose()
        {
            Disable();
            _comps.Dispose();
            OnDispose();

            OnClear();
            _comps = null;
            _enabled = false;
            _owner = null;
            _disposed = true;
            // ReferencePool.Release(this);
        }

        public void SetOwner(ICompOwner owner)
        {
            _disposed = false;
            _owner = owner;
            _comps = CompCollection.Create();
            try
            {
                OnAdd(owner);
            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        public void Enable()
        {
            if (_enabled)
                return;

            _enabled = true;
            OnEnable();
        }

        public void Disable()
        {
            if (!_enabled)
                return;

            _enabled = false;
            OnDisable();
        }

        public void SetEnabled(bool enabled)
        {
            if (enabled)
                Enable();
            else
                Disable();
        }

        public void Update(float deltaTime)
        {
            if (_enabled)
            {
                try
                {
                    OnUpdate(deltaTime);
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            }
        }

        public void FixedUpdate()
        {
            if (_enabled)
            {
                try
                {
                    OnFixedUpdate();
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            }
        }

        public void LateUpdate()
        {
            if (_enabled)
            {
                try
                {
                    OnLateUpdate();
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            }
        }

        public void LateFixedUpdate()
        {
            if (_enabled)
            {
                try
                {
                    OnLateFixedUpdate();
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            }
        }

        void IReference.Clear()
        {
            OnClear();
            _comps = null;
            _enabled = false;
            _owner = null;
        }

        protected virtual void OnCreate()
        {
        }

        protected virtual void OnDispose()
        {
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnAdd(ICompOwner owner)
        {
        }

        public virtual void OnRemove()
        {
        }

        protected virtual void OnUpdate(float deltaTime)
        {
        }

        protected virtual void OnFixedUpdate()
        {
        }

        protected virtual void OnLateUpdate()
        {
        }

        protected virtual void OnLateFixedUpdate()
        {
        }

        protected abstract void OnClear();
    }

}