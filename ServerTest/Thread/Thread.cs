namespace Server3.Thread
{

    public class Thread : IReference
    {
        protected bool _isRun = false;
        protected System.Threading.Thread _thread;

        public bool IsRun => _isRun;

        public virtual void Dispose()
        {
            Stop();
            _thread.Join();
        }

        public virtual bool Start()
        {
            _isRun = true;
            _thread = new System.Threading.Thread(() =>
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
    }

}