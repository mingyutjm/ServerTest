using System.Net.Sockets;
using Server3;

namespace login
{

    public class LoginObj : ISocketObject
    {
        private string _account;
        private string _password;
        private Socket _socket;

        public string Account => _account;
        public Socket Socket => _socket;

        public LoginObj(Socket socket, string account, string password)
        {
            _socket = socket;
            _account = account;
            _password = password;
        }
    }

}