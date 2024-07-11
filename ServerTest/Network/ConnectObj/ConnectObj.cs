using System.Net.Sockets;

namespace Server3;

using TotalSizeType = ushort;

/// <summary>
/// 连接对象
/// 1. 所有网络数据放入buffer
/// 2. 
/// </summary>
public class ConnectObj : IReference
{
    protected Network _network;
    protected Socket _socket;
    protected RecvBuffer _recvBuffer;
    protected SendBuffer _sendBuffer;
    protected bool isClosed = false;

    public Socket Socket => _socket;
    public bool IsClosed => isClosed;

    public ConnectObj(Network network, Socket socket)
    {
        _network = network;
        _socket = socket;

        _recvBuffer = new RecvBuffer(Const.DefaultRecvBufferSize, this);
        _sendBuffer = new SendBuffer(Const.DefaultSendBufferSize, this);
    }

    public void Dispose()
    {
        _socket.Close();
        _recvBuffer.Dispose();
        _sendBuffer.Dispose();
    }

    public void Close()
    {
        isClosed = true;
    }

    /// <summary>
    /// 接收RecvBuffer的数据，并让ThreadMgr分发数据
    /// </summary>
    /// <returns>RecvBuffer内是否有新消息处理</returns>
    public bool Recv()
    {
        bool hasRes = false;
        Span<byte> writeBuf = null;
        while (true)
        {
            // 总空间数据不足一个头的大小，扩容
            if (_recvBuffer.GetEmptySize() < (PacketHead.Size + sizeof(TotalSizeType)))
            {
                _recvBuffer.ReAllocBuffer();
            }

            int emptySize = _recvBuffer.GetWriteBuffer(out writeBuf);
            try
            {
                int dataSize = _socket.Receive(writeBuf, SocketFlags.None, out SocketError errorCode);
                if (dataSize > 0)
                {
                    Log.Info($"recv size: {dataSize}");
                    _recvBuffer.FillData(dataSize);
                }
                else if (dataSize == 0)
                {
                    if (errorCode == SocketError.Interrupted || errorCode == SocketError.WouldBlock)
                    {
                        hasRes = true;
                        break;
                    }

                    Log.Error($"recv size: {dataSize}, error: {errorCode}");
                    break;
                }
                else
                {
                    if (errorCode == SocketError.Interrupted || errorCode == SocketError.WouldBlock)
                    {
                        hasRes = true;
                        break;
                    }

                    Log.Error($"recv size: {dataSize}, error: {errorCode}");
                    break;
                }
            }
            catch (Exception e)
            {
                Log.Exception(e);
                break;
            }
        }

        if (hasRes)
        {
            while (true)
            {
                var packet = _recvBuffer.GetPacket();
                if (packet == null)
                    break;
                ThreadMgr.Instance.DispatchPacket(packet);
            }
        }
        return hasRes;
    }

    /// <summary>
    /// 发送SendBuffer内的数据
    /// </summary>
    /// <returns>SendBuffer内是否有数据要发送</returns>
    public bool Send()
    {
        while (true)
        {
            Span<byte> pBuffer = null;
            int needSendSize = _sendBuffer.GetReadBuffer(out pBuffer);

            // 没有数据可发送
            if (needSendSize <= 0)
                return true;

            try
            {
                int size = _socket.Send(pBuffer, SocketFlags.None, out SocketError errorCode);
                if (size > 0)
                {
                    Log.Info($"send size: {needSendSize}");
                    _sendBuffer.RemoveData(size);

                    // 下一帧再发送
                    if (size < needSendSize)
                        return true;
                }
            }
            catch (Exception e)
            {
                if (e is SocketException se)
                {
                    Log.Exception(se);
                }
                else if (e is ObjectDisposedException ode)
                {
                    Log.Exception(ode);
                }
                else
                {
                    Log.Exception(e);
                }
                return false;
            }
        }
    }

    /// <summary>
    /// Buffer内是否有数据待接收
    /// </summary>
    /// <returns></returns>
    public bool HasRecvData()
    {
        return _recvBuffer.HasData();
    }

    /// <summary>
    /// SendBuffer内是否有数据待发送
    /// </summary>
    /// <returns></returns>
    public bool HasSendData()
    {
        return _sendBuffer.HasData();
    }

    public Packet? GetRecvPacket()
    {
        return _recvBuffer.GetPacket();
    }

    /// <summary>
    /// 将Packet放入sendBuffer
    /// </summary>
    /// <param name="packet"></param>
    public void SendPacket(Packet packet)
    {
        _sendBuffer.AddPacket(packet);
    }
}