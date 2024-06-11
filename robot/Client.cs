using System.Buffers;
using Server3;

namespace robot
{

    public class Client : NetworkConnector
    {
        private int _msgCount;
        private int _index;
        private bool _isCompleted;
        private byte[]? _lastMsg;

        private System.Random _rand;

        public bool IsCompleted => _isCompleted;

        public Client(int msgCount)
        {
            _rand = new Random();
            _msgCount = msgCount;
        }

        private void GetRandom(byte[] randBytes)
        {
            _rand.NextBytes(randBytes);
        }

        public void DataHandler()
        {
            if (_isCompleted)
                return;

            if (!IsConnected())
                return;

            if (_index < _msgCount)
            {
                // 发送数据
                if (_lastMsg == null)
                {
                    _lastMsg = ArrayPool<byte>.Shared.Rent(_rand.Next(1, 10));
                    GetRandom(_lastMsg);
                    Log.Info($"send. size: {_lastMsg.Length}, msg: {_lastMsg.ToArray().ToStr()}");

                    Packet packet = new Packet(1);
                    packet.AddBuffer(_lastMsg, _lastMsg.Length);
                    SendPacket(packet);

                    ArrayPool<byte>.Shared.Return(_lastMsg);
                }
                else
                {
                    if (HasRecvData())
                    {
                        Packet? packet = GetRecvPacket();
                        if (packet != null)
                        {
                            Span<byte> msg = new Span<byte>(packet.GetBuffer(), 0, packet.GetDataLength());
                            Log.Info($"recv size {msg.Length}, msg: {msg.ToArray().ToStr()}");

                            // if (msg != _lastMsg)
                            //     Log.Error(" !!!!!!!!!!!!!!!!! error.");

                            _lastMsg = null;
                            ++_index;
                        }
                    }
                }
            }
            else
            {
                _isCompleted = true;
            }
        }
    }

}