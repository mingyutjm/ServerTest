using System.Buffers;

namespace Server3
{

    public class ThreadObjectList : SnObject, IReference
    {
        protected CacheRefresh<ThreadObject> _objects = CacheRefresh<ThreadObject>.Create();
        protected CacheSwap<Packet> _packets = CacheSwap<Packet>.Create();
        protected List<ThreadObject> _cacheDeleteList = new List<ThreadObject>(16);

        public void AddObject(ThreadObject obj)
        {
            lock (_objects)
            {
                if (!obj.Init())
                {
                    Log.Error($"AddObject failed. ThreadObject init failed");
                }
                else
                {
                    obj.RegisterMsgFunction();
                    _objects.AddCache.Add(obj);
                    if (this is GameThread thread)
                    {
                        obj.SetThread(thread);
                    }
                }
            }
        }

        public void AddPacketToList(Packet packet)
        {
            lock (_packets)
            {
                _packets.WriteCache.Add(packet);
            }
        }

        public void Tick()
        {
            ThreadObject[]? tmpList = null;
            lock (_objects)
            {
                if (_objects.CanSwap())
                {
                    _objects.Swap(_cacheDeleteList);
                    foreach (var toDelete in _cacheDeleteList)
                    {
                        toDelete.Dispose();
                    }
                }
            }

            lock (_packets)
            {
                if (_packets.CanSwap())
                {
                    _packets.Swap();
                }
            }

            var objList = _objects.ReadCache;
            var msgList = _packets.ReadCache;

            foreach (var obj in objList)
            {
                foreach (var packet in msgList)
                {
                    if (obj.IsFollowMsgId(packet))
                    {
                        obj.ProcessPacket(packet);
                    }
                }
                obj.Tick();

                if (!obj.IsActive)
                {
                    _objects.RemoveCache.Add(obj);
                }
            }

            msgList.Clear();
        }

        public virtual void Dispose()
        {
            lock (_objects)
            {
                _objects.Dispose();
            }

            lock (_packets)
            {
                _packets.Dispose();
            }

        }
    }

}