using System.Net.Sockets;
using Server3;

namespace login
{

    public class LoginObjMgr : IReference
    {
        private Dictionary<string, Socket> _accounts = new Dictionary<string, Socket>();
        private Dictionary<Socket, LoginObj> _players = new Dictionary<Socket, LoginObj>();

        public void Dispose()
        {
            _players.Clear();
        }

        public void AddPlayer(Socket socket, string account, string password)
        {
            if (_players.TryAdd(socket, new LoginObj(socket, account, password)))
            {
                _accounts[account] = socket;
            }
            else
            {
                Log.Error($"Player {account} already exist");
            }
        }

        public void RemovePlayer(Socket socket)
        {
            if (_players.TryGetValue(socket, out LoginObj? player))
            {
                _accounts.Remove(player.Account);
                _players.Remove(socket);
            }
            else
            {
                Log.Error($"Player not exist");
            }
        }

        public bool TryQueryPlayer(Socket socket, out LoginObj? player)
        {
            return _players.TryGetValue(socket, out player);
        }

        public bool TryQueryPlayer(string account, out LoginObj? player)
        {
            player = default;
            if (_accounts.TryGetValue(account, out Socket? socket))
            {
                return TryQueryPlayer(socket, out player);
            }
            return false;
        }
    }

}