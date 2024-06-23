namespace Server3
{

    public class GameThread : ThreadObjectList, IReference
    {
        protected bool _isRun = true;
        protected Thread _thread;

        private List<ThreadObject> _objects = new List<ThreadObject>(4);

        public bool IsRun => _isRun;

        public virtual void Dispose()
        {
            foreach (var obj in _objects)
            {
                obj.Dispose();
            }
            _objects.Clear();
            Stop();
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
            if (_isRun)
            {
                _isRun = false;
                _thread.Join();
            }
        }
    }

}