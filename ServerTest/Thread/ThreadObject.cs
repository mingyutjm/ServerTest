using Server3.Message;

namespace Server3
{

    public abstract class ThreadObject : MessageList
    {
        protected bool _isActive = true;
        protected GameThread _gameThread;
        public bool IsActive => _isActive;

        public abstract bool Init();
        public abstract void RegisterMsgFunction();
        public abstract void Tick();

        public override void Dispose()
        {
            base.Dispose();
        }

        public void SetThread(GameThread gameThread)
        {
            _gameThread = gameThread;
        }

        public GameThread GetThread()
        {
            return _gameThread;
        }
    }

}