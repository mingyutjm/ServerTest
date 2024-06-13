namespace Server3
{

    public abstract class ThreadObject
    {
        protected bool _isActive;
        public bool IsActive => _isActive;

        public abstract bool Init();
        public abstract void RegisterMsgFunction();
        public abstract void Tick();
    }

}