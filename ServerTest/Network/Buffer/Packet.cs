using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MemoryPack;

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

public class Packet : Buffer, ISocketObject
{
    private int _msgId;
    private Socket _socket;

    public int MsgId => _msgId;
    public Socket Socket => _socket;

    public Packet() : this(0, null)
    {
    }

    public static Packet Create(int msgId, Socket socket)
    {
        Packet packet = new Packet(msgId, socket);
        return packet;
    }

    private Packet(int msgId, Socket socket)
    {
        _msgId = msgId;
        _socket = socket;
        CleanBuffer();
        _beginIndex = 0;
        _endIndex = 0;
        _buffer = ByteArrayPool.Rent(Const.DefaultPacketBufferSize);
        _bufferSize = _buffer.Length;
    }

    public override void Dispose()
    {
        _msgId = 0;
        base.Dispose();
    }

    public void CleanBuffer()
    {
        if (_buffer != null)
            ByteArrayPool.Return(_buffer, true);
        _beginIndex = 0;
        _endIndex = 0;
        _bufferSize = 0;
    }

    public byte[] GetBuffer()
    {
        return _buffer;
    }

    public int GetDataLength()
    {
        return _endIndex - _beginIndex;
    }

    public void FillData(int size)
    {
        _endIndex += size;
    }

    public void ReAllocBuffer()
    {
        base.ReAllocBuffer(_endIndex - _beginIndex);
    }

    public void SerializeToBuffer<T>(T obj)
    {
        byte[] binData = MemoryPackSerializer.Serialize(obj);
        // AddBuffer(binData, binData.Length);
        while (GetEmptySize() < binData.Length)
        {
            ReAllocBuffer();
        }
        Array.Copy(binData, _buffer, binData.Length);
        FillData(binData.Length);
    }

    public T? Deserialize<T>()
    {
        Span<byte> bufferSpan = new Span<byte>(_buffer, 0, GetDataLength());
        T? obj = MemoryPackSerializer.Deserialize<T>(bufferSpan);
        return obj;
    }

    // private void AddBuffer(byte[] source, int size)
    // {
    //     while (GetEmptySize() < size)
    //     {
    //         ReAllocBuffer();
    //     }
    //     Array.Copy(source, _buffer, size);
    //     FillData(size);
    // }
}