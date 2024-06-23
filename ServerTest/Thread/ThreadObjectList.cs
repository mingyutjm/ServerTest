using System.Buffers;

namespace Server3
{

    public class ThreadObjectList : SnObject
    {
        protected List<ThreadObject> _objects = new List<ThreadObject>(4);
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
                    if (this is GameThread thread)
                    {
                        obj.SetThread(thread);
                    }
                }
            }
        }

        public void AddPacketToList(Packet packet)
        {
            lock (_locker)
            {
                foreach (var obj in _objects)
                {
                    if (obj.IsFollowMsgId(packet))
                    {
                        obj.AddPacket(packet);
                    }
                }
            }
        }

        public void Tick()
        {
            ThreadObject[]? tmpList = null;
            lock (_locker)
            {
                if (_objects.Count == 0)
                    return;

                tmpList = ArrayPool<ThreadObject>.Shared.Rent(_objects.Count);
                for (int i = 0; i < _objects.Count; i++)
                    tmpList[i] = _objects[i];
            }

            foreach (var obj in tmpList)
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
            ArrayPool<ThreadObject>.Shared.Return(tmpList);
            Thread.Sleep(1);
        }
    }

}