namespace Server3
{

    public class GameThread : SnObject, IReference
    {
        protected bool _isRun = false;
        protected Thread _thread;

        private List<ThreadObject> _objects = new List<ThreadObject>(4);
        private List<ThreadObject> _objectsTemp = new List<ThreadObject>(4);
        private object _locker = new object();

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

        public virtual void Tick()
        {
            lock (_locker)
            {
                _objectsTemp.AddRange(_objects);
            }

            for (int i = _objects.Count - 1; i >= 0; i--)
            {
                var obj = _objects[i];
                obj.ProcessPacket();
                obj.Tick();
                if (!obj.IsActive)
                {
                    lock (_locker)
                    {
                        _objects.RemoveAt(i);
                    }
                    obj.Dispose();
                }
            }

            _objectsTemp.Clear();
            Thread.Sleep(1);
        }

        public void AddThreadObj(ThreadObject obj)
        {
            obj.RegisterMsgFunction();
            lock (_locker)
            {
                _objects.Add(obj);
            }
        }

        public void AddPacket(Packet packet)
        {
            lock (_locker)
            {
                // lock
                foreach (var obj in _objects)
                {
                    if (obj.IsFollowMsgId(packet.MsgId))
                    {
                        obj.AddPacket(packet);
                    }
                }
            }
        }
    }

}