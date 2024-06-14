﻿namespace Server3
{

    public abstract class ServerApp
    {
        protected AppType _appType;
        protected Global _global;
        protected ThreadMgr _threadMgr;

        public AppType AppType => _appType;
        public Global Global => _global;
        public ThreadMgr ThreadMgr => _threadMgr;

        public ServerApp(AppType appType)
        {
            _appType = appType;

            _global = Global.Create();
            _threadMgr = ThreadMgr.Create();

            UpdateTime();

            // 创建线程
            for (int i = 0; i < 3; i++)
            {
                _threadMgr.NewThread();
            }
        }

        public abstract void InitApp();

        public void Shutdown()
        {
            _threadMgr.Shutdown();
        }

        public void StartAllThread()
        {
            _threadMgr.StartAllThread();
        }

        public void Run()
        {
            bool isRun = true;
            while (isRun)
            {
                UpdateTime();
                Thread.Sleep(10);
                isRun = _threadMgr.IsGameLoop();
            }
        }

        protected void UpdateTime()
        {
            var timeValue = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Global.Instance.timeTick = (ulong)timeValue;
            var now = DateTime.Now;
            Global.Instance.yearDay = now.DayOfYear;
        }

        public bool AddListenerToThread(string ip, int port)
        {
            NetworkListener netListen = new NetworkListener();
            if (!netListen.Listen(ip, port))
            {
                netListen.Dispose();
                return false;
            }
            _threadMgr.AddObjToThread(netListen);
            return true;
        }
    }

}