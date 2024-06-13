using System.Runtime.CompilerServices;

namespace Server3;

using TotalSizeType = ushort;

public class SendBuffer : NetBuffer
{
    public SendBuffer(int size) : base(size)
    {
    }

    public int GetReadBuffer(out Span<byte> buffer)
    {
        int readSize = GetReadSize();
        buffer = new Span<byte>(_buffer, _beginIndex, readSize);
        return readSize;
    }

    public void AddPacket(Packet packetToAdd)
    {
        int dataLength = packetToAdd.GetDataLength();
        int packetHeadSize = Unsafe.SizeOf<PacketHead>();
        int totalSize = dataLength + packetHeadSize + sizeof(TotalSizeType);

        // 长度不够，扩容
        while (GetEmptySize() < totalSize)
        {
            ReAllocBuffer();
        }

        // 1.整体长度
        byte[] tempBytes = ByteArrayPool.Rent(sizeof(TotalSizeType));
        Unsafe.As<byte, int>(ref tempBytes[0]) = totalSize;
        CopyFrom(tempBytes, sizeof(TotalSizeType));
        ByteArrayPool.Return(tempBytes, true);

        // 2.头部
        // tempBytes = ByteArrayPool.Rent(packetHeadSize);
        byte[] tempBytes2 = ByteArrayPool.Rent(packetHeadSize);
        PacketHead head = new PacketHead() { msgId = (ushort)packetToAdd.MsgId };
        CopyFrom(head.ToBytes(ref tempBytes2), packetHeadSize);
        // ByteArrayPool.Return(tempBytes, true);
        ByteArrayPool.Return(tempBytes2, true);

        // 3.数据
        CopyFrom(packetToAdd.GetBuffer(), packetToAdd.GetDataLength());
    }

    /// 将 source 拷贝到 _buffer
    private void CopyFrom(byte[] source, int size)
    {
        int writeSize = GetWriteSize();
        if (writeSize < size)
        {
            // 1.copy到尾部
            Array.Copy(source, 0, _buffer, _endIndex, writeSize);

            // 2.copy到头部
            Array.Copy(source, writeSize, _buffer, 0, size - writeSize);
        }
        else
        {
            Array.Copy(source, 0, _buffer, _endIndex, size);
        }
        FillData(size);
    }
}