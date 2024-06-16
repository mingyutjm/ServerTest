using System.Runtime.CompilerServices;

namespace Server3
{

    public class NetBuffer : Buffer
    {
        // 在环形中，极端情况下 _endIndex 可能与 _beginIndex 重合
        // 重合时有两种可能，一种是没有数据，另一种是满数据
        // 有效数据长度
        protected int _dataSize;
        protected ConnectObj _connectObj;

        public NetBuffer(int size, ConnectObj conn)
        {
            _connectObj = conn;
            _beginIndex = 0;
            _endIndex = 0;
            _dataSize = 0;
            _buffer = ByteArrayPool.Rent(size);
            _bufferSize = _buffer.Length;
        }

        public bool HasData()
        {
            if (_dataSize <= 0)
                return false;

            // 至少要有一个协议头
            if (_dataSize < Unsafe.SizeOf<PacketHead>())
                return false;

            return true;
        }

        // 包括环的头与环的尾一共的空字节数
        public override int GetEmptySize()
        {
            return _bufferSize - _dataSize;
        }

        // 当前可写长度
        public int GetWriteSize()
        {
            // 只返回尾部的长度
            if (_beginIndex <= _endIndex)
            {
                return _bufferSize - _endIndex;
            }
            else
            {
                return _beginIndex - _endIndex;
            }
        }

        // 当前可读长度, 数据长度
        public int GetReadSize()
        {
            if (_dataSize <= 0)
                return 0;

            if (_beginIndex < _endIndex)
            {
                return _endIndex - _beginIndex;
            }
            // 只返回尾部的数据长度
            else
            {
                return _bufferSize - _beginIndex;
            }
        }

        // 操作 _endIndex 往右
        public void FillData(int size)
        {
            _dataSize += size;
            // 往_endIndex后面添
            if (_bufferSize - _endIndex <= size)
            {
                size -= _bufferSize - _endIndex;
                _endIndex = 0;
            }
            _endIndex += size;
        }

        // 操作 _beginIndex 往右
        public void RemoveData(int size)
        {
            _dataSize -= size;
            if (_beginIndex + size >= _bufferSize)
            {
                size -= _bufferSize - _beginIndex;
                _beginIndex = 0;
            }
            _beginIndex += size;
        }

        public void ReAllocBuffer()
        {
            base.ReAllocBuffer(_dataSize);
        }
    }

}