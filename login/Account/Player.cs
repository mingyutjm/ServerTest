using System.Net.Sockets;
using Server3;

namespace login
{

    public class Player : ISocketObject
    {
        private string _account;
        private string _password;
        private Socket _socket;

        public string Account => _account;

        public Player(Socket socket, string account, string password)
        {
            _socket = socket;
            _account = account;
            _password = password;
        }

        public Socket GetSocket()
        {
            return _socket;
        }
    }

}