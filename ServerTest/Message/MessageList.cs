namespace Server3.Message
{
    public class MessageList : IReference
    {
        public delegate void HandleFunction(Packet p);

        // protected object _locker = new object();
        // protected List<Packet> _msgList = new List<Packet>(4);
        // protected List<Packet> _msgListCopy = new List<Packet>(4);
        // protected Dictionary<int, HandleFunction> _callbackHandle = new Dictionary<int, HandleFunction>();

        protected MessageCallbackFunctionInfo? _callbacks;

        public virtual void Dispose()
        {
            // foreach (var pac in _msgList)
            // {
            //     pac.Dispose();
            // }
            // _msgList.Clear();
            // _msgListCopy.Clear();
            // _callbackHandle.Clear();
            _callbacks?.Dispose();
        }

        public void AttachCallbackHandler(MessageCallbackFunctionInfo callback)
        {
            _callbacks = callback;
        }

        // public void RegisterFunction(int msgId, HandleFunction handle)
        // {
        //     lock (_locker)
        //     {
        //         _callbackHandle[msgId] = handle;
        //     }
        // }

        public bool IsFollowMsgId(Packet packet)
        {
            return _callbacks?.IsFollowMsgId(packet) ?? false;
        }

        public void ProcessPacket(Packet packet)
        {
            _callbacks?.ProcessPackets();
        }

        public void AddPacket(Packet packet)
        {
            _callbacks?.AddPacket(packet);
        }

        public static void DispatchPacket(Packet packet)
        {
            ThreadMgr.Instance.DispatchPacket(packet);
        }

        public static void SendPacket(Packet packet)
        {
            ThreadMgr.Instance.SendPacket(packet);
        }
    }
}