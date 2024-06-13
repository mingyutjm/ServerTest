using Server3;
using Server3;

namespace robot
{

    public class ClientThread : GameThread
    {
        private int _msgCount;
        private Client? _client;

        public ClientThread(int msgCount)
        {
            _msgCount = msgCount;
        }

        public override void Tick()
        {
            if (_client == null)
            {
                _client = new Client(_msgCount);
                if (!_client.Connect("127.0.0.1", 2233))
                {
                    Log.Error("ClientThread Connect failed.");
                }
            }
            else
            {
                _client.Tick();
                _client.DataHandler();

                if (_client.IsCompleted)
                    Stop();
            }

            System.Threading.Thread.Sleep(1000);
        }

        public override void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }
            base.Dispose();
        }
    }

}