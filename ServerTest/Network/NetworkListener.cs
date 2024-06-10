using System.Net;
using System.Net.Sockets;

namespace Server3
{

    public class NetworkListener : Network
    {
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
                    Socket newSocket = _masterSocket.Accept();
                    SetSocketOpt(newSocket);
                    ConnectObj newConn = new ConnectObj(this, newSocket);
                    _connects.Add(newSocket, newConn);
                    acceptCount++;
                }
                catch (SocketException e)
                {
                    return acceptCount;
                }
            }
        }

        public bool Tick()
        {
            bool rt = Select();
            if (_readFds.Contains(_masterSocket))
                Accept();
            return rt;
        }
    }

}