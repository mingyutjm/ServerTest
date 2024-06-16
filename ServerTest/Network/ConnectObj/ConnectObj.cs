using System.Net.Sockets;

namespace Server3;

using TotalSizeType = ushort;

public class ConnectObj : IReference
{
    protected Network _network;
    protected Socket _socket;
    protected RecvBuffer _recvBuffer;
    protected SendBuffer _sendBuffer;

    public Socket Socket => _socket;

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

    public bool HasRecvData()
    {
        return _recvBuffer.HasData();
    }

    public Packet? GetRecvPacket()
    {
        return _recvBuffer.GetPacket();
    }

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

        if (hasRes)
        {
            while (true)
            {
                var packet = _recvBuffer.GetPacket();
                if (packet == null)
                    break;
                ThreadMgr.Instance.AddPacket(packet);
            }
        }
        return hasRes;
    }

    public bool HasSendData()
    {
        return _sendBuffer.HasData();
    }

    public void SendPacket(Packet packet)
    {
        _sendBuffer.AddPacket(packet);
    }

    public bool Send()
    {
        while (true)
        {
            Span<byte> pBuffer = null;
            int needSendSize = _sendBuffer.GetReadBuffer(out pBuffer);

            // 没有数据可发送
            if (needSendSize <= 0)
                return true;

            int size = _socket.Send(pBuffer, SocketFlags.None, out SocketError errorCode);
            if (size > 0)
            {
                Log.Info($"send size: {needSendSize}");
                _sendBuffer.RemoveData(size);

                // 下一帧再发送
                if (size < needSendSize)
                    return true;
            }

            if (size == -1)
            {
                Log.Error($"needSendSize: {needSendSize}, error: {errorCode}");
                return false;
            }
        }
    }
}