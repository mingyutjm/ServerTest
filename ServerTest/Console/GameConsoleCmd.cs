namespace Server3
{

    public delegate void HandleConsole(string a, string b);

    public abstract class GameConsoleCmd : IReference
    {
        private Dictionary<string, HandleConsole> _handles = new Dictionary<string, HandleConsole>();

        public abstract void RegisterHandler();

        public virtual void Dispose()
        {
            _handles.Clear();
        }

        public void Process(Span<string> paras) 
        {
            if (paras.Length <= 1)
                return;

            string key = paras[1];
            if (_handles.TryGetValue(key, out var handle))
            {
                if (paras.Length == 2)
                {
                    handle.Invoke(string.Empty, string.Empty);
                }
                else if (paras.Length == 3)
                {
                    handle.Invoke(paras[2], string.Empty);
                }
                else if (paras.Length == 4)
                {
                    handle.Invoke(paras[2], paras[3]);
                }
                else
                {
                    Log.Error("input error, -help for help.");
                }
            }
        }

        protected void RegisterHandler_Impl(string key, HandleConsole handler)
        {
            _handles[key] = handler;
        }
    }

}