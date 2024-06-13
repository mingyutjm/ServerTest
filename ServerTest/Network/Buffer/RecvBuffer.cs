using System.Runtime.CompilerServices;

namespace Server3;

using TotalSizeType = ushort;

public class RecvBuffer : NetBuffer
{
    public RecvBuffer(int size) : base(size)
    {
    }

    public new void Dispose()
    {
    }

    // 获取写buffer
    public int GetWriteBuffer(out Span<byte> buffer)
    {
        int writeSpace = GetWriteSize();
        buffer = new Span<byte>(_buffer, _endIndex, writeSpace);
        return writeSpace;
    }

    public Packet? GetPacket()
    {
        // 数据长度不够
        if (_dataSize < sizeof(TotalSizeType))
            return null;

        // 1.读出 整体长度
        byte[] tempBytes = ByteArrayPool.Rent(sizeof(TotalSizeType));
        CopyTo(tempBytes, sizeof(TotalSizeType));
        ushort totalSize = BitConverter.ToUInt16(tempBytes);
        ByteArrayPool.Return(tempBytes, true);

        // 协议体长度不够，等待
        if (_dataSize < totalSize)
            return null;

        RemoveData(sizeof(TotalSizeType));

        // 2.读出 PacketHead
        int packetHeadSize = Unsafe.SizeOf<PacketHead>();
        tempBytes = ByteArrayPool.Rent(packetHeadSize);
        CopyTo(tempBytes, packetHeadSize);
        PacketHead head = PacketHead.FromBytes(tempBytes);
        RemoveData(packetHeadSize);
        ByteArrayPool.Return(tempBytes, true);

        // 3.读出 协议
        Packet? newPacket = Packet.Create(head.msgId);
        int dataLength = totalSize - packetHeadSize - sizeof(TotalSizeType);
        while (newPacket.TotalSize < dataLength)
        {
            newPacket.ReAllocBuffer();
        }

        CopyTo(newPacket.GetBuffer(), dataLength);
        newPacket.FillData(dataLength);
        RemoveData(dataLength);

        return newPacket;
    }

    /// 将 _buffer 拷贝到 target
    private void CopyTo(byte[] target, int length)
    {
        int readSize = GetReadSize();
        if (readSize < length)
        {
            // 1.copy尾部数据
            Array.Copy(_buffer, _beginIndex, target, 0, readSize);

            // 2.copy头部数据
            Array.Copy(_buffer, 0, target, readSize, length - readSize);
        }
        else
        {
            Array.Copy(_buffer, _beginIndex, target, 0, length);
        }
    }
}