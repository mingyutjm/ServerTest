namespace Server3
{

    public class ThreadMgr : ThreadObjectList, IMgr<ThreadMgr>
    {
        // Mgr
        protected bool _isInit;
        protected static ThreadMgr s_instance;

        public static ThreadMgr Create()
        {
            if (s_instance == null)
                s_instance = new ThreadMgr();
            return s_instance;
        }

        public static ThreadMgr Instance => s_instance ?? throw new Exception("Instance is not init");

        private ulong _lastThreadSn = 0;
        private object _threadLocker = new object();
        private List<GameThread> _threads = new List<GameThread>(4);

        private object _locatorLocker = new object();
        private Dictionary<AppType, Network> _networkLocator = new Dictionary<AppType, Network>(1);

        public void Init()
        {
            if (_isInit)
                return;
            _isInit = true;
        }

        public void Shutdown()
        {
            _isInit = false;
            foreach (var thread in _threads)
            {
                thread.Dispose();
            }
            _threads.Clear();
        }

        public void StartAllThread()
        {
            foreach (var thread in _threads)
            {
                thread.Start();
            }
        }

        public bool IsGameLoop()
        {
            foreach (var thread in _threads)
            {
                if (thread.IsRun)
                    return true;
            }
            return false;
        }

        public void NewThread()
        {
            lock (_threadLocker)
            {
                GameThread gameThread = new GameThread();
                _threads.Add(gameThread);
            }
        }

        /// <summary>
        /// 找一个GameThread包裹住ThreadObject
        /// 找的过程负载均衡
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool AddObjToThread(ThreadObject obj)
        {
            lock (_threadLocker)
            {
                // 在加入之前初始化一下
                if (!obj.Init())
                {
                    Log.Error("AddThreadObj Failed. ThreadObject init failed.");
                    return false;
                }

                if (_threads.Count == 0)
                {
                    Log.Error("AddThreadObj Failed. no thead.");
                    return false;
                }
                // 找到上一次的线程
                int lastThreadIndex = 0;
                if (_lastThreadSn > 0)
                {
                    lastThreadIndex = FindIndexSn(_lastThreadSn);
                    if (lastThreadIndex == -1)
                    {
                        Log.Error("AddThreadObj Failed. cant find last thread.");
                        return false;
                    }
                }

                // 取到它的下一个活动线程
                for (int i = 0; i < _threads.Count; i++)
                {
                    ++lastThreadIndex;
                    if (lastThreadIndex >= _threads.Count)
                        lastThreadIndex = 0;
                    if (_threads[lastThreadIndex].IsRun)
                        break;
                }

                var findThread = _threads[lastThreadIndex];
                findThread.AddObject(obj);
                _lastThreadSn = findThread.Sn;
            }
            return true;
        }

        public void AddNetworkToThread(AppType appType, Network network)
        {
            if (!AddObjToThread(network))
                return;
            lock (_locatorLocker)
            {
                _networkLocator.TryAdd(appType, network);
            }
        }

        public bool TryGetNetwork(AppType appType, out Network network)
        {
            lock (_locatorLocker)
            {
                return _networkLocator.TryGetValue(appType, out network);
            }
        }

        public void DispatchPacket(Packet packet)
        {
            // 主线程
            AddPacketToList(packet);

            // 子线程
            lock (_threadLocker)
            {
                foreach (var thread in _threads)
                {
                    thread.AddPacketToList(packet);
                }
            }
        }

        public void SendPacket(Packet packet)
        {
            if (TryGetNetwork(AppType.Listen, out Network network))
            {
                if (network is NetworkListener listener)
                {
                    listener.SendPacket(packet);
                }
            }
        }

        private int FindIndexSn(ulong sn)
        {
            for (int i = 0; i < _threads.Count; i++)
            {
                if (_threads[i].Sn == sn)
                    return i;
            }
            return -1;
        }
    }

}