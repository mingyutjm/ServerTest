using System.Buffers;

namespace Server3;

public class Buffer : IReference
{
    protected static ArrayPool<byte> ByteArrayPool = ArrayPool<byte>.Shared;

    protected byte[] _buffer;
    protected int _bufferSize = 0;
    protected int _beginIndex = 0;
    protected int _endIndex = 0;

    public int EndIndex => _endIndex;
    public int BeginIndex => _beginIndex;
    public int TotalSize => _bufferSize;

    public virtual int GetEmptySize()
    {
        return _bufferSize - _endIndex;
    }

    public void ReAllocBuffer(int dataLength)
    {
        if (_bufferSize >= Const.MaxBufferSize)
        {
            Log.Error($"Alloc buffer exceed max size!");
        }

        var temp = ByteArrayPool.Rent((_bufferSize + Const.AdditionalSize));
        int newEndIndex;
        // 没循环
        if (_beginIndex < _endIndex)
        {
            Array.Copy(_buffer, _beginIndex, temp, 0, _endIndex - _beginIndex);
            newEndIndex = _endIndex - _beginIndex;
        }
        // 有循环
        else
        {
            // 没有数据
            if (_beginIndex == _endIndex && dataLength <= 0)
            {
                newEndIndex = 0;
            }
            // 有数据 (数据在两端)
            else
            {
                // 1.先COPY尾部
                Array.Copy(_buffer, _beginIndex, temp, 0, _bufferSize - _beginIndex);
                newEndIndex = _bufferSize - _beginIndex;

                // 2.再COPY头部
                if (_endIndex > 0)
                {
                    Array.Copy(_buffer, 0, temp, newEndIndex, _endIndex);
                    newEndIndex += _endIndex;
                }
            }
        }

        // 修改数据
        _bufferSize += Const.AdditionalSize;
        ByteArrayPool.Return(_buffer, true);
        _buffer = temp;

        _beginIndex = 0;
        _endIndex = newEndIndex;
    }

    public void Dispose()
    {
    }
}