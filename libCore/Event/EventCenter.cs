namespace LibCore
{
    // public delegate void MEventHandler(MEventArgs e);
    //
    public abstract class MEventArgs : IReference
    {
        public static readonly MEventArgs Empty;
        public abstract void Clear();
    }

    public interface IEventInfo
    {
    }

    public class EventInfo : IEventInfo
    {
        public MAction actions;

        public EventInfo(MAction action)
        {
            actions += action;
        }
    }

    public class EventInfo<T> : IEventInfo
    {
        public MAction<T> actions;

        public EventInfo(MAction<T> action)
        {
            actions += action;
        }
    }

    public class EventInfo<T1, T2> : IEventInfo
    {
        public MAction<T1, T2> actions;

        public EventInfo(MAction<T1, T2> action)
        {
            actions += action;
        }
    }

    public class EventInfo<T1, T2, T3> : IEventInfo
    {
        public MAction<T1, T2, T3> actions;

        public EventInfo(MAction<T1, T2, T3> action)
        {
            actions += action;
        }
    }

    /// <summary>
    /// Event name must be organized.
    /// </summary>
    public class EventCenter
    {
        private readonly Dictionary<string, IEventInfo> _eventDic;

        public EventCenter()
        {
            _eventDic = new Dictionary<string, IEventInfo>();
        }

        public void Subscribe(string name, MAction action)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo evt)
                    evt.actions += action;
                else
                    Log.Error($"AddListener Failed, EventId: {name}, expect type<>, find type<{_eventDic[name].GetType()}");
            }
            else
            {
                _eventDic.Add(name, new EventInfo(action));
            }
        }

        public void Subscribe<T>(string name, MAction<T> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo<T> evt)
                    evt.actions += action;
                else
                    Log.Error($"AddListener Failed, EventId: {name}, expect type<{typeof(T)}>, find type<{_eventDic[name].GetType()}");
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T>(action));
            }
        }

        public void Subscribe<T1, T2>(string name, MAction<T1, T2> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo<T1, T2> evt)
                    evt.actions += action;
                else
                    Log.Error(
                        $"AddListener Failed, EventId: {name}, expect type<{typeof(T1)}, {typeof(T2)}>, find type<{_eventDic[name].GetType()}");
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T1, T2>(action));
            }
        }

        public void Subscribe<T1, T2, T3>(string name, MAction<T1, T2, T3> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo<T1, T2, T3> evt)
                    evt.actions += action;
                else
                    Log.Error(
                        $"AddListener Failed, EventId: {name}, expect type<{typeof(T1)}, {typeof(T2)}, {typeof(T2)}>, find type<{_eventDic[name].GetType()}");
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T1, T2, T3>(action));
            }
        }

        public void Unsubscribe(string name, MAction action)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo evt)
                {
                    evt.actions -= action;
                }
            }
        }

        public void Unsubscribe<T>(string name, MAction<T> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo<T> evt)
                {
                    evt.actions -= action;
                }
            }
        }

        public void Unsubscribe<T1, T2>(string name, MAction<T1, T2> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo<T1, T2> evt)
                {
                    evt.actions -= action;
                }
            }
        }

        public void Unsubscribe<T1, T2, T3>(string name, MAction<T1, T2, T3> action)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo<T1, T2, T3> evt)
                {
                    evt.actions -= action;
                }
            }
        }

        public void SendEvent(string name)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo evt)
                {
                    try
                    {
                        evt.actions?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Log.Exception(e);
                    }
                }
                else
                {
                    Log.Error($"SendEvent failed: {name}, Type mismatch");
                }
            }
        }

        public void SendEvent<T>(string name, T info = default)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo<T> evt)
                {
                    try
                    {
                        evt.actions?.Invoke(info);
                    }
                    catch (Exception e)
                    {
                        Log.Exception(e);
                    }
                }
                else
                {
                    Log.Error($"SendEvent failed: {name}, Type \"<{typeof(T).Name}>\" mismatch");
                }
            }
        }

        public void SendEvent<T1, T2>(string name, T1 info1, T2 info2)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo<T1, T2> evt)
                {
                    try
                    {
                        evt.actions?.Invoke(info1, info2);
                    }
                    catch (Exception e)
                    {
                        Log.Exception(e);
                    }
                }
                else
                {
                    Log.Error($"SendEvent failed: {name}, Type \"<{typeof(T1).Name}, {typeof(T2).Name}>\" mismatch");
                }
            }
        }

        public void SendEvent<T1, T2, T3>(string name, T1 info1, T2 info2, T3 info3)
        {
            if (_eventDic.ContainsKey(name))
            {
                if (_eventDic[name] is EventInfo<T1, T2, T3> evt)
                {
                    try
                    {
                        evt.actions?.Invoke(info1, info2, info3);
                    }
                    catch (Exception e)
                    {
                        Log.Exception(e);
                    }
                }
                else
                {
                    Log.Error(
                        $"SendEvent failed: {name}, Type \"<{typeof(T1).Name}, {typeof(T2).Name}, {typeof(T3).Name}>\" mismatch");
                }
            }
        }

        public void Clear()
        {
            _eventDic.Clear();
        }
    }
}