namespace Server3
{

    public class GameConsole : ThreadObject
    {
        private bool _isRun = true;

        protected Dictionary<string, GameConsoleCmd> _handles = new Dictionary<string, GameConsoleCmd>();
        protected Queue<string> _commands = new Queue<string>();
        protected Thread _thread;
        protected object _locker = new object();

        public override bool Init()
        {
            _thread = new Thread(() =>
            {
                while (_isRun)
                {
                    string? cmd = System.Console.ReadLine();
                    if (!string.IsNullOrEmpty(cmd))
                    {
                        lock (_locker)
                        {
                            _commands.Enqueue(cmd);
                        }
                    }
                }
            });
            _thread.Start();
            return true;
        }

        public override void Dispose()
        {
            foreach (var (_, handle) in _handles)
            {
                handle.Dispose();
            }
            _handles.Clear();
            _isRun = false;
            _thread.Join();
            base.Dispose();
        }

        public override void RegisterMsgFunction()
        {
        }

        public override void Tick()
        {
            string cmd = null;
            lock (_locker)
            {
                if (!_commands.TryDequeue(out cmd))
                    return;
            }
            Span<string> cmds = cmd.Split(' ', StringSplitOptions.TrimEntries).AsSpan();
            if (cmds.Length <= 0)
                return;
            string key = cmds[0];
            if (_handles.TryGetValue(key, out var handle))
            {
                handle.Process(cmds);
            }
        }

        protected void Register<T>(string cmd) where T : GameConsoleCmd, new()
        {
            T handle = new T();
            handle.RegisterHandler();
            _handles.Add(cmd, handle);
        }
    }

}