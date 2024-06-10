using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Server3;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct PacketHead
{
    [FieldOffset(0)] public ushort msgId;

    public static int Size => Unsafe.SizeOf<PacketHead>();

    public static PacketHead FromBytes(byte[] data)
    {
        PacketHead head = new PacketHead();
        head.msgId = BitConverter.ToUInt16(data);
        return head;
    }

    public byte[] ToBytes(ref byte[] data)
    {
        Unsafe.As<byte, ushort>(ref data[0]) = msgId;
        return data;
    }
};

public class Packet : Buffer
{
    private int _msgId;

    public int MsgId => _msgId;

    public Packet() : this(0)
    {
    }

    public Packet(int msgId)
    {
        _msgId = msgId;
        CleanBuffer();
        _bufferSize = Const.DefaultPacketBufferSize;
        _beginIndex = 0;
        _endIndex = 0;
        _buffer = ByteArrayPool.Rent(_bufferSize);
    }

    public new void Dispose()
    {
        _msgId = 0;
        _beginIndex = 0;
        _endIndex = 0;
    }

    public void CleanBuffer()
    {
        if (_buffer != null)
        {
            ByteArrayPool.Return(_buffer, true);
        }

        _beginIndex = 0;
        _endIndex = 0;
        _bufferSize = 0;
    }

    public byte[] GetBuffer()
    {
        return _buffer;
    }

    public void AddBuffer(byte[] source, int size)
    {
        while (GetEmptySize() < size)
        {
            ReAllocBuffer();
        }
        Array.Copy(source, _buffer, size);
        FillData(size);
    }

    public int GetDataLength()
    {
        return _endIndex - _beginIndex;
    }

    public void FillData(int size)
    {
    }

    public void ReAllocBuffer()
    {
    }
}