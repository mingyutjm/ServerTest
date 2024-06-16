using Server3.Message;

namespace Server3
{

    public abstract class ThreadObject : MessageList, IReference
    {
        protected bool _isActive = true;
        public bool IsActive => _isActive;

        public abstract bool Init();
        public abstract void Dispose();
        public abstract void RegisterMsgFunction();
        public abstract void Tick();
    }

}