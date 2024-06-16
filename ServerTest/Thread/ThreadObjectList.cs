namespace Server3
{

    public class ThreadObjectList : SnObject
    {
        protected List<ThreadObject> _objects = new List<ThreadObject>(4);
        protected List<ThreadObject> _objectsCopy = new List<ThreadObject>(4);
        protected object _locker = new object();

        public void AddObject(ThreadObject obj)
        {
            lock (_locker)
            {
                if (!obj.Init())
                {
                    Log.Error($"AddObject failed. ThreadObject init failed");
                }
                else
                {
                    obj.RegisterMsgFunction();
                    _objects.Add(obj);
                }
            }
        }

        public void AddPacketToList(Packet packet)
        {
            lock (_locker)
            {
                foreach (var obj in _objects)
                {
                    if (obj.IsFollowMsgId(packet.MsgId))
                    {
                        obj.AddPacket(packet);
                    }
                }
            }
        }

        public void Tick()
        {
            lock (_locker)
            {
                _objectsCopy.AddRange(_objects);
            }

            foreach (var obj in _objectsCopy)
            {
                obj.ProcessPacket();
                obj.Tick();

                if (!obj.IsActive)
                {
                    lock (_locker)
                    {
                        _objects.Remove(obj);
                        obj.Dispose();
                    }
                }
            }

            Thread.Sleep(1);
        }
    }

}