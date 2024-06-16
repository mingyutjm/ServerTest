using System.Net.Sockets;

namespace Server3
{

    public interface ISocketObject
    {
        public Socket GetSocket();
    }

}