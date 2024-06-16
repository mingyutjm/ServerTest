namespace Server3
{

    public abstract class Singleton<T> where T : new()
    {
        protected static T s_instance;

        public static T Create()
        {
            if (s_instance == null)
                s_instance = new T();
            return s_instance;
        }

        public static T Instance => s_instance ?? throw new Exception("Instance is not init");
    }

    public interface IMgr<T>
    {
        public void Init();
        public void Shutdown();
        // public void Tick(float deltaTime);
    }

    public abstract class Mgr<T> : Singleton<T>, IMgr<T> where T : new()
    {
        protected bool _isInit;

        public void Init()
        {
            if (_isInit)
                return;
            _isInit = true;
            OnInit();
        }

        public void Shutdown()
        {
            _isInit = false;
            OnShutdown();
        }

        public void Tick(float deltaTime)
        {
            if (!_isInit)
                return;
            OnTick(deltaTime);
        }

        protected abstract void OnInit();
        protected abstract void OnShutdown();

        protected virtual void OnTick(float deltaTime)
        {
        }
    }

}