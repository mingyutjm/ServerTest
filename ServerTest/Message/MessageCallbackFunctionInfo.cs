using System.Buffers;
using System.Net.Sockets;
using System.Runtime.InteropServices.JavaScript;

namespace Server3.Message
{

    public abstract class MessageCallbackFunctionInfo : IReference
    {
        protected object _msgLocker = new object();
        protected List<Packet> _msgList = new List<Packet>();

        public abstract bool IsFollowMsgId(Packet packet);
        public abstract void ProcessPackets();

        public void AddPacket(Packet packet)
        {
            lock (_msgLocker)
            {
                _msgList.Add(packet);
            }
        }

        public void Dispose()
        {
        }
    }

    public class MessageCallbackFunction : MessageCallbackFunctionInfo
    {
        public delegate void HandleFunction(Packet p);

        protected Dictionary<int, HandleFunction> _callbacks = new Dictionary<int, HandleFunction>();

        public Dictionary<int, HandleFunction> GetCallbacks()
        {
            return _callbacks;
        }

        public void RegisterFunction(int msgId, HandleFunction func)
        {
            lock (_msgLocker)
            {
                _callbacks[msgId] = func;
            }
        }

        public override bool IsFollowMsgId(Packet packet)
        {
            lock (_msgLocker)
            {
                return _callbacks.ContainsKey(packet.MsgId);
            }
        }

        public override void ProcessPackets()
        {
            Packet[]? tmpList = null;
            lock (_msgLocker)
            {
                if (_msgList.Count == 0)
                    return;
                tmpList = ArrayPool<Packet>.Shared.Rent(_msgList.Count);
                for (int i = 0; i < _msgList.Count; i++)
                    tmpList[i] = _msgList[i];
                _msgList.Clear();
            }
            foreach (var packet in tmpList)
            {
                if (packet == null)
                    continue;

                if (_callbacks.TryGetValue(packet.MsgId, out var handleFunc))
                {
                    handleFunc.Invoke(packet);
                }
            }
            ArrayPool<Packet>.Shared.Return(tmpList);
        }
    }

    public class MessageCallbackFunctionFilterObj<T> : MessageCallbackFunction
    {
        public delegate void HandleFunctionWithObj(T obj, Packet packet);
        public delegate T HandleGetObject(Socket socket);

        private Dictionary<int, HandleFunctionWithObj> _callbackWithObjs = new Dictionary<int, HandleFunctionWithObj>();

        public HandleGetObject? GetPacketObject;

        public void RegisterFunctionWithObj(int msgId, HandleFunctionWithObj callback)
        {
            _callbackWithObjs[msgId] = callback;
        }

        public override bool IsFollowMsgId(Packet packet)
        {
            if (base.IsFollowMsgId(packet))
                return true;

            if (_callbackWithObjs.TryGetValue(packet.MsgId, out var _))
            {
                if (GetPacketObject == null)
                    return false;
                T obj = GetPacketObject(packet.Socket);
                if (obj != null)
                    return true;
            }
            return false;
        }

        public override void ProcessPackets()
        {
            Packet[]? tmpList = null;
            lock (_msgLocker)
            {
                if (_msgList.Count == 0)
                    return;
                tmpList = ArrayPool<Packet>.Shared.Rent(_msgList.Count);
                for (int i = 0; i < _msgList.Count; i++)
                    tmpList[i] = _msgList[i];
                _msgList.Clear();
            }

            foreach (var packet in tmpList)
            {
                if (packet == null)
                    continue;

                if (_callbacks.TryGetValue(packet.MsgId, out var callback))
                {
                    callback(packet);
                    continue;
                }

                if (_callbackWithObjs.TryGetValue(packet.MsgId, out var callbackWithObj))
                {
                    if (GetPacketObject == null)
                        continue;

                    T obj = GetPacketObject(packet.Socket);
                    if (obj != null)
                    {
                        callbackWithObj(obj, packet);
                    }
                }
            }
            ArrayPool<Packet>.Shared.Return(tmpList);
        }

        public void CopyFrom(MessageCallbackFunction callbacks)
        {
            var copyFromData = callbacks.GetCallbacks();

            foreach (var (msgId, func) in copyFromData)
            {
                _callbacks.Add(msgId, func);
            }
        }
    }

}