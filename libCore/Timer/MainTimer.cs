namespace LibCore
{

    public class MainTimer
    {
        private const int DefaultSize = 16;

        private const uint Idmask_FrameLoop = 1;
        private const uint Idmask_FrameOnce = 2;
        private const uint Idmask_FrameTicker = 3;
        private const uint Idmask_TimeOnce = 4;
        private const uint Idmask_TimeTicker = 5;
        private const uint Idmask_TimeMain = 6;
        private const uint Idmask_TimeLoop = 7;

        private FrameLoop _frameLoop;
        private FrameOnce _frameOnce;
        private FrameTicker _frameTicker;
        private TimeOnce _timeOnce;
        private TimeTicker _timeTicker;
        private TimeLoop _timeLoop;

        private Action[] _mainList;
        private Action[] _oldMainList;
        private int _pos;

        public MainTimer()
        {
            _frameLoop = new FrameLoop();
            _frameOnce = new FrameOnce();
            _frameTicker = new FrameTicker();
            _timeOnce = new TimeOnce();
            _timeTicker = new TimeTicker();
            _timeLoop = new TimeLoop();

            _pos = 0;
            _mainList = new Action[DefaultSize];
            _oldMainList = new Action[DefaultSize];
        }

        /// <summary>
        /// 主线程执行，线程安全
        /// </summary>
        /// <param name="callback"></param>
        public void ToMain(Action callback)
        {
            lock (_mainList)
            {
                Grow();
                _mainList[_pos] = callback;
                _pos++;
            }
        }

        /// <summary>
        /// 主线程调用，下一帧执行
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public uint NextFrame(Action callback)
        {
            return (_frameOnce.Add(callback) << 4) | Idmask_FrameOnce;
        }

        /// <summary>
        /// 主线程调用，每帧执行一次，可以执行times次
        /// </summary>
        /// <param name="times"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public uint FrameTicker(int times, Action<int> callback)
        {
            if (times <= 0)
            {
                return 0;
            }
            return (_frameTicker.Add(times, callback) << 4) | Idmask_FrameTicker;
        }

        /// <summary>
        /// 主线程调用，每帧都执行，类似monobehave Update
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public uint FrameLoop(Action callback)
        {
            return (_frameLoop.Add(callback) << 4) | Idmask_FrameLoop;
        }

        /// <summary>
        /// 主线程调用，每duration秒执行一次, 小于1帧时候实际效果是一帧
        /// </summary>
        /// <param name="duration">秒</param>
        /// <param name="callback">回调</param>
        /// <returns></returns>
        public uint TimeLoop(float duration, Action callback)
        {
            return (_timeLoop.Add(duration, callback) << 4) | Idmask_TimeLoop;
        }

        /// <summary>
        /// 延迟time秒执行
        /// </summary>
        /// <param name="time"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public uint After(float time, Action callback)
        {
            return (_timeOnce.Add(time, callback) << 4) | Idmask_TimeOnce;
        }

        /// <summary>
        /// 延迟gap秒执行一次，一共执行times次数
        /// </summary>
        /// <param name="gap"></param>
        /// <param name="times"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public uint Ticker(float gap, int times, Action<int> callback)
        {
            if (times <= 0)
            {
                return 0;
            }
            return (_timeTicker.Add(gap, times, callback) << 4) | Idmask_TimeTicker;
        }

        /// <summary>
        /// 提前删除
        /// </summary>
        /// <param name="id"></param>
        public void Remove(uint id)
        {
            switch (id & 0xf)
            {
                case Idmask_FrameLoop:
                    _frameLoop.Remove(id >> 4);
                    break;
                case Idmask_FrameOnce:
                    _frameOnce.Remove(id >> 4);
                    break;
                case Idmask_FrameTicker:
                    _frameTicker.Remove(id >> 4);
                    break;
                case Idmask_TimeOnce:
                    _timeOnce.Remove(id >> 4);
                    break;
                case Idmask_TimeTicker:
                    _timeTicker.Remove(id >> 4);
                    break;
                case Idmask_TimeLoop:
                    _timeLoop.Remove(id >> 4);
                    break;
                default: break;
            }
        }

        internal void Update()
        {
            _frameOnce.Update();
            _frameLoop.Update();
            _frameTicker.Update();
            _timeOnce.Update();
            
            _timeTicker.Update();
            _timeLoop.Update();

            UpdateMain();
        }

        /// <summary>
        /// 清除所有定时器
        /// </summary>
        public void Clean()
        {
            _frameOnce.Clean();
            _frameLoop.Clean();
            _frameTicker.Clean();
            _timeOnce.Clean();
            _timeTicker.Clean();
        }

        private void Grow()
        {
            if (_pos + 1 < _mainList.Length)
                return;

            Action[] tmp = new Action[_mainList.Length * 2];
            Array.Copy(_mainList, tmp, _pos);
            _mainList = tmp;
        }

        private void UpdateMain()
        {
            if (_pos == 0)
                return;

            int pos;
            Action[] tmp = null;
            lock (_mainList)
            {
                pos = _pos;
                _pos = 0;
                tmp = _oldMainList;
                _oldMainList = _mainList;
                _mainList = tmp;
            }

            for (int i = 0; i < pos; i++)
            {
                try
                {
                    _oldMainList[i]();
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            }
        }
    }

}