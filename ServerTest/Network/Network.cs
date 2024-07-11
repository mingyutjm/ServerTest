using System.Buffers;
using System.Net.Sockets;
using Server3.Message;

namespace Server3;

/// <summary>
/// 1. 创建Socket，设置Socket，
/// 2. 添加和删除ConnectObject对象
/// 3. 通过Select调用ConnectObject的Recv和Send
/// </summary>
public abstract class Network : ThreadObject, ISocketObject
{
    protected Socket? _masterSocket;
    protected Dictionary<Socket, ConnectObj> _connects = new Dictionary<Socket, ConnectObj>();
    protected object _sendMsgLocker = new object();
    protected List<Packet> _sendMsgList = new List<Packet>();
    protected bool _isBroadCast = true;

    // fd_set, readfds, writefds, exceptfds
    protected List<Socket?> _readFds = new List<Socket?>();
    protected List<Socket?> _writeFds = new List<Socket?>();
    protected List<Socket?> _exceptFds = new List<Socket?>();

    private List<Socket> _waitingRemoveConn = new List<Socket>(4);

    public Socket Socket => _masterSocket;
    public bool IsBroadCast => _isBroadCast;

    public override void Dispose()
    {
        foreach (var (socket, conn) in _connects)
        {
            conn.Dispose();
        }
        _connects.Clear();
        _masterSocket?.Close();
        _masterSocket = null;
        base.Dispose();
    }

    public override void Tick()
    {
        Packet[]? tmpList = null;
        lock (_sendMsgLocker)
        {
            if (_sendMsgList.Count == 0)
                return;
            tmpList = ArrayPool<Packet>.Shared.Rent(_sendMsgList.Count);
            for (int i = 0; i < _sendMsgList.Count; i++)
                tmpList[i] = _sendMsgList[i];
            _sendMsgList.Clear();
        }
        foreach (var p in tmpList)
        {
            if (_connects.TryGetValue(p.Socket, out var conn))
            {
                conn.SendPacket(p);
            }
        }
        ArrayPool<Packet>.Shared.Return(tmpList);
    }

    public bool Select()
    {
        _readFds.Clear();
        _writeFds.Clear();
        _exceptFds.Clear();

        _readFds.Add(_masterSocket);
        _writeFds.Add(_masterSocket);
        _exceptFds.Add(_masterSocket);

        // Remove closed conn
        foreach (var (socket, conn) in _connects)
        {
            if (conn.IsClosed)
            {
                _waitingRemoveConn.Add(socket);
            }
        }
        foreach (var socket in _waitingRemoveConn)
        {
            _connects.Remove(socket);
        }
        _waitingRemoveConn.Clear();

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

    public override void RegisterMsgFunction()
    {
        var pMsgCallBack = new MessageCallbackFunction();
        AttachCallbackHandler(pMsgCallBack);
        pMsgCallBack.RegisterFunction((int)MsgId.NetworkDisconnectToNet, HandleDisconnect);
    }

    public new void SendPacket(Packet p)
    {
        lock (_sendMsgLocker)
        {
            _sendMsgList.Add(p);
        }
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

    private void HandleDisconnect(Packet packet)
    {
        if (_connects.TryGetValue(packet.Socket, out var conn))
        {
            conn.Close();
        }
    }
}