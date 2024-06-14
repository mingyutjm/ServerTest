using System.Net.Sockets;

namespace Server3
{

    public class NetworkConnector : Network
    {
        private string _ip;
        private int _port;
        private byte[] _testError = new byte[4];

        public override bool Init()
        {
            return true;
        }

        public override void RegisterMsgFunction()
        {
        }

        public bool Connect(string ip, int port)
        {
            _ip = ip;
            _port = port;

            _masterSocket = CreateSocket();
            if (_masterSocket == null)
                return false;

            try
            {
                _masterSocket.Connect(ip, port);
                CreateConnectObj(_masterSocket);
            }
            catch (Exception e)
            {
                // ignored
            }
            return true;
        }

        public bool IsConnected()
        {
            return _connects.Count > 0;
        }

        public override void Tick()
        {
            bool selectSuccess = Select();
            if (!IsConnected())
            {
                if (_exceptFds.Contains(_masterSocket))
                {
                    Log.Error($"connect except. socket: {_masterSocket}, re connect");
                    Dispose();
                    Connect(_ip, _port);
                    return;
                }

                if (_readFds.Contains(_masterSocket) || _writeFds.Contains(_masterSocket))
                {
                    try
                    {
                        _masterSocket!.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error, _testError);
                        CreateConnectObj(_masterSocket);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"connect failed. socket: {_masterSocket}, re connect");

                        // 关闭当前socket，重新connect
                        Dispose();
                        Connect(_ip, _port);
                    }
                    // finally
                    // {
                    //     if (_testError[0] == 0)
                    //     {
                    //         ConnectObj conn = new ConnectObj(this, _masterSocket!);
                    //         _connects.Add(_masterSocket, conn);
                    //     }
                    //     else
                    //     {
                    //         Log.Error($"connect failed. socket: {_masterSocket}, re connect");
                    //
                    //         // 关闭当前socket，重新connect
                    //         Dispose();
                    //         Connect(_ip, _port);
                    //     }
                    // }
                }
            }
        }

        public bool HasRecvData()
        {
            if (!IsConnected())
                return false;

            if (_connects.Count != 1)
            {
                Log.Error("Error. NetworkConnector has two connect!!!!");
                return false;
            }

            ConnectObj? conn = GetConnectObj();
            return conn != null && conn.HasRecvData();
        }

        public Packet? GetRecvPacket()
        {
            ConnectObj? conn = GetConnectObj();
            return conn?.GetRecvPacket();
        }

        public void SendPacket(Packet sendPacket)
        {
            ConnectObj? conn = GetConnectObj();
            conn?.SendPacket(sendPacket);
        }

        private ConnectObj? GetConnectObj()
        {
            return _connects[_masterSocket!];
        }
    }

}