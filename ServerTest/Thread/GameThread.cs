namespace Server3
{

    public class GameThread : ThreadObjectList, IReference
    {
        public enum State
        {
            Init,
            Run,
            Stop
        }

        protected State _state = State.Init;
        protected Thread _thread;

        public bool IsRun => _state == State.Run;

        public virtual bool Dispose()
        {
            if (!_thread.IsAlive)
                return true;
            foreach (var obj in _objects)
            {
                obj.Dispose();
            }
            _objects.Clear();
            Stop();
        }

        public virtual bool Start()
        {
            _thread = new Thread(() =>
            {
                _state = State.Run;
                while (_state == State.Run && !Global.Instance.IsStop)
                {
                    Tick();
                    Thread.Sleep(1);
                }
            });
            _thread.Start();
            return true;
        }

        public void Stop()
        {
            _state = State.Stop;
        }
    }

}