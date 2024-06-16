using System.Net.Sockets;

namespace Server3;

public abstract class Network : ThreadObject, ISocketObject
{
    protected Socket? _masterSocket;
    protected Dictionary<Socket, ConnectObj> _connects = new Dictionary<Socket, ConnectObj>();

    // fd_set, readfds, writefds, exceptfds
    protected List<Socket?> _readFds = new List<Socket?>();
    protected List<Socket?> _writeFds = new List<Socket?>();
    protected List<Socket?> _exceptFds = new List<Socket?>();

    public Socket GetSocket() => _masterSocket;

    public override void Dispose()
    {
        foreach (var (socket, conn) in _connects)
        {
            conn.Dispose();
        }
        _connects.Clear();
        _masterSocket?.Close();
        _masterSocket = null;
    }

    public bool Select()
    {
        _readFds.Clear();
        _writeFds.Clear();
        _exceptFds.Clear();

        _readFds.Add(_masterSocket);
        _writeFds.Add(_masterSocket);
        _exceptFds.Add(_masterSocket);

        foreach (var (socket, conn) in _connects)
        {
            _readFds.Add(socket);
            _exceptFds.Add(socket);

            if (conn.HasSendData())
            {
                _writeFds.Add(socket);
            }
            else
            {
                if (socket == _masterSocket)
                {
                    _writeFds.Remove(socket);
                }
            }
        }

        int timeout = 1000;
        Socket.Select(_readFds, _writeFds, _exceptFds, timeout);
        foreach (var (socket, conn) in _connects)
        {
            if (_exceptFds.Contains(socket))
            {
                Log.Error($"socket except!! socket {socket}");
                _connects[socket].Dispose();
                _connects.Remove(socket);
                continue;
            }

            if (_readFds.Contains(socket))
            {
                if (!conn.Recv())
                {
                    conn.Dispose();
                    _connects.Remove(socket);
                    continue;
                }
            }

            if (_writeFds.Contains(socket))
            {
                if (!conn.Send())
                {
                    conn.Dispose();
                    _connects.Remove(socket);
                    continue;
                }
            }
        }

        return true;
    }

    public void CreateConnectObj(Socket socket)
    {
        ConnectObj conn = new ConnectObj(this, socket);
        _connects.Add(socket, conn);
    }

    protected static void SetSocketOpt(Socket socket)
    {
        // 1.端口关闭后马上重新启用
        bool isReuseaddr = true;
        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, isReuseaddr);

        // 2.发送、接收timeout
        int netTimeout = 3000;
        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, netTimeout);
        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, netTimeout);

        // 3.非阻塞
        socket.Blocking = false;
    }

    protected Socket? CreateSocket()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        SetSocketOpt(socket);
        return socket;
    }
}