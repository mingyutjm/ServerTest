namespace Server3.Message
{

    public class MessageList
    {
        public delegate void HandleFunction(Packet p);

        protected object _locker = new object();
        protected List<Packet> _msgList = new List<Packet>(4);
        protected List<Packet> _msgListCopy = new List<Packet>(4);
        protected Dictionary<int, HandleFunction> _callbackHandle = new Dictionary<int, HandleFunction>();

        public void RegisterFunction(int msgId, HandleFunction handle)
        {
            lock (_locker)
            {
                _callbackHandle[msgId] = handle;
            }
        }

        public bool IsFollowMsgId(int msgId)
        {
            lock (_locker)
            {
                return _callbackHandle.ContainsKey(msgId);
            }
        }

        public void ProcessPacket()
        {
            lock (_locker)
            {
                _msgListCopy.AddRange(_msgList);
                _msgList.Clear();
            }

            foreach (var packet in _msgListCopy)
            {
                if (_callbackHandle.TryGetValue(packet.MsgId, out var handleFunc))
                {
                    handleFunc(packet);
                }
                else
                {
                    Log.Warning($"packet has no handler. msg id {packet.MsgId}");
                }
            }
            _msgListCopy.Clear();
        }

        public void AddPacket(Packet packet)
        {
            // lock
            lock (_locker)
            {
                _msgList.Add(packet);
            }
        }
    }

}