using System.Net;
using System.Net.Sockets;

namespace Server3
{

    /// <summary>
    /// 网络监听对象，用于服务器监听Socket用
    /// 1. Tick Select
    /// 2. 监听Socket
    /// 3. Accept new socket
    /// </summary>
    public class NetworkListener : Network
    {
        public override bool Init()
        {
            return true;
        }

        public override void RegisterMsgFunction()
        {
        }

        public override void Tick()
        {
            bool rt = Select();
            if (_readFds.Contains(_masterSocket))
                Accept();
        }

        public bool Listen(string ip, int port)
        {
            _masterSocket = CreateSocket();
            if (_masterSocket == null)
                return false;

            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

            try
            {
                _masterSocket.Bind(endPoint);
            }
            catch (Exception e)
            {
                // ignored
                Log.Exception(e);
            }

            int backlog = 10;
            _masterSocket.Listen(backlog);
            return true;
        }

        protected virtual int Accept()
        {
            int acceptCount = 0;
            while (true)
            {
                try
                {
                    Socket newSocket = _masterSocket!.Accept();
                    SetSocketOpt(newSocket);
                    CreateConnectObj(newSocket);
                    acceptCount++;
                }
                catch (SocketException e)
                {
                    return acceptCount;
                }
            }
        }
    }

}