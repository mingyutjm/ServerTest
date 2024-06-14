namespace Server3
{

    public class GameThread : SnObject, IReference
    {
        protected bool _isRun = false;
        protected Thread _thread;

        private List<ThreadObject> _objects;
        private List<ThreadObject> _waitingAddObjects;
        private object _locker = new object();
        private bool _isTick = false;

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
            _isRun = false;
            _thread.Join();
        }

        public virtual void Tick()
        {
            _isTick = true;
            for (int i = _objects.Count; i >= 0; i--)
            {
                var obj = _objects[i];
                obj.Tick();
                if (!obj.IsActive)
                {
                    obj.Dispose();
                    _objects.RemoveAt(i);
                }
            }
            _isTick = false;

            foreach (var obj in _waitingAddObjects)
            {
                _objects.Add(obj);
            }
            _waitingAddObjects.Clear();

            Thread.Sleep(1);
        }

        public void AddThreadObj(ThreadObject obj)
        {
            obj.RegisterMsgFunction();
            if (_isTick)
            {
                _waitingAddObjects.Add(obj);
            }
            else
            {
                _objects.Add(obj);
            }
        }
    }

}