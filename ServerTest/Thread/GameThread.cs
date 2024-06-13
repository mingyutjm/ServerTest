namespace Server3
{

    public class GameThread : SnObject, IReference
    {
        protected bool _isRun = false;
        protected Thread _thread;

        private List<ThreadObject> _objects;
        private List<ThreadObject> _tmpObjects;
        private object _locker = new object();

        public bool IsRun => _isRun;

        public virtual void Dispose()
        {
            Stop();
            _thread.Join();
        }

        public virtual bool Start()
        {
            _isRun = true;
            _thread = new Thread(() =>
            {
                while (_isRun)
                {
                    Tick();
                }
            });
            _thread.Start();
            return true;
        }

        public void Stop()
        {
            _isRun = false;
        }

        public virtual void Tick()
        {
        }

        public void AddThreadObj(ThreadObject obj)
        {
        }
    }

}