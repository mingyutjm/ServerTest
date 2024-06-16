using System.Net.Sockets;
using Server3;

namespace login
{

    public class PlayerMgr : IReference
    {
        private Dictionary<string, Socket> _accounts;
        private Dictionary<Socket, Player> _players;

        public void Dispose()
        {
            _players.Clear();
        }

        public void AddPlayer(Socket socket, string account, string password)
        {
            if (_players.TryAdd(socket, new Player(socket, account, password)))
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
            if (_players.TryGetValue(socket, out Player? player))
            {
                _accounts.Remove(player.Account);
                _players.Remove(socket);
            }
            else
            {
                Log.Error($"Player not exist");
            }
        }

        public bool TryQueryPlayer(Socket socket, out Player? player)
        {
            return _players.TryGetValue(socket, out player);
        }

        public bool TryQueryPlayer(string account, out Player? player)
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