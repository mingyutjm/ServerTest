namespace Server3
{

    public class Global : Singleton<Global>
    {
        private object _locker = new object();
        private uint _serverId = 0;
        private uint _snTicket = 1;
        private bool _isStop = false;

        public ulong timeTick;
        public int yearDay;
        public bool IsStop => _isStop;

        public ulong GenerateSn()
        {
            lock (_locker)
            {
                ulong ret = (timeTick << 32) + (_serverId << 16) + _snTicket;
                _snTicket += 1;
                return ret;
            }
        }

        public void Stop()
        {
            _isStop = true;
        }
    }

}