using System.Runtime.CompilerServices;

namespace Server3;

using TotalSizeType = ushort;

public class SendBuffer : NetBuffer
{
    public SendBuffer(int size) : base(size)
    {
    }

    public new void Dispose()
    {
    }

    public int GetBuffer(out Span<byte> buffer)
    {
        if (_dataSize <= 0)
        {
            buffer = default;
            return 0;
        }

        if (_beginIndex < _endIndex)
        {
            int length = _endIndex - _beginIndex;
            buffer = new Span<byte>(_buffer, _beginIndex, length);
            return length;
        }
        else
        {
            int length = _bufferSize - _beginIndex;
            buffer = new Span<byte>(_buffer, _beginIndex, length);
            return length;
        }
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
        MemcpyToBuffer(tempBytes, sizeof(TotalSizeType));
        ByteArrayPool.Return(tempBytes, true);

        // 2.头部
        // tempBytes = ByteArrayPool.Rent(packetHeadSize);
        byte[] tempBytes2 = ByteArrayPool.Rent(packetHeadSize);
        PacketHead head = new PacketHead() { msgId = (ushort)packetToAdd.MsgId };
        MemcpyToBuffer(head.ToBytes(ref tempBytes2), packetHeadSize);
        // ByteArrayPool.Return(tempBytes, true);
        ByteArrayPool.Return(tempBytes2, true);

        // 3.数据
        MemcpyToBuffer(packetToAdd.GetBuffer(), packetToAdd.GetDataLength());
    }

    // 将 source 拷贝到 _buffer
    private void MemcpyToBuffer(byte[] source, int size)
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