namespace Server3
{

    public class ThreadMgr : Mgr<ThreadMgr>
    {
        private ulong _lastThreadSn = 0;
        private object _locker = new object();
        private List<GameThread> _threads;

        protected override void OnInit()
        {
        }

        protected override void OnShutdown()
        {
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
            lock (_locker)
            {
                GameThread gameThread = new GameThread();
                _threads.Add(gameThread);
            }
        }

        public void AddObjToThread(ThreadObject obj)
        {
            lock (_locker)
            {
                // 在加入之前初始化一下
                if (!obj.Init())
                {
                    Log.Error("AddThreadObj Failed. ThreadObject init failed.");
                    return;
                }

                if (_threads.Count == 0)
                {
                    Log.Error("AddThreadObj Failed. no thead.");
                    return;
                }
                // 找到上一次的线程
                int lastThreadIndex = 0;
                if (_lastThreadSn > 0)
                {
                    lastThreadIndex = FindIndexSn(_lastThreadSn);
                    if (lastThreadIndex == -1)
                    {
                        Log.Error("AddThreadObj Failed. cant find last thread.");
                        return;
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
                findThread.AddThreadObj(obj);
                _lastThreadSn = findThread.Sn;
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