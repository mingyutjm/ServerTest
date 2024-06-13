using Server3;

namespace server
{

    public class Server : NetworkListener
    {
        protected int _acceptCount = 0;
        protected int _sendMsgCount = 0;
        protected int _recvMsgCount = 0;
        protected bool _isShow = true;

        public bool DataHandler()
        {
            foreach (var (socket, conn) in _connects)
            {
                HandleOne(conn);
            }

            if (_isShow)
            {
                _isShow = false;
                Log.Warning($"accept {_acceptCount}, recv count: {_recvMsgCount}, send count: {_sendMsgCount}");
            }
            return true;
        }

        protected override int Accept()
        {
            int rs = base.Accept();
            _acceptCount += rs;
            _isShow = true;
            return rs;
        }

        protected void HandleOne(ConnectObj conn)
        {
            // 收到客户端的消息，马上原样发送出去
            while (conn.HasRecvData())
            {
                Packet? packet = conn.GetRecvPacket();
                if (packet == null)
                {
                    // 数据不全，下帧再检查
                    Log.Info($"HandleOne break");
                    break;
                }

                Span<byte> msg = new Span<byte>(packet.GetBuffer(), 0, packet.GetDataLength());
                Log.Info($"recv size {msg.Length}, msg: {msg.ToArray().ToStr()}");
                conn.SendPacket(packet);
                packet.Dispose();
                ++_recvMsgCount;
                ++_sendMsgCount;
                _isShow = true;
            }
        }
    }

}