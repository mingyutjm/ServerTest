namespace LibCore.Event
{

    /// <summary>
    /// Global event center, send event cross modules.
    /// </summary>
    public static class EventManager
    {
        private static EventCenter _eventCenter;

        static EventManager()
        {
            _eventCenter = new EventCenter();
        }

        public static void Subscribe(string name, MAction action)
        {
            _eventCenter.Subscribe(name, action);
        }

        public static void Subscribe<T>(string name, MAction<T> action)
        {
            _eventCenter.Subscribe(name, action);
        }

        public static void Subscribe<T1, T2>(string name, MAction<T1, T2> action)
        {
            _eventCenter.Subscribe(name, action);
        }

        public static void Subscribe<T1, T2, T3>(string name, MAction<T1, T2, T3> action)
        {
            _eventCenter.Subscribe(name, action);
        }

        public static void Unsubscribe(string name, MAction action)
        {
            _eventCenter.Unsubscribe(name, action);
        }

        public static void Unsubscribe<T>(string name, MAction<T> action)
        {
            _eventCenter.Unsubscribe(name, action);
        }

        public static void Unsubscribe<T1, T2>(string name, MAction<T1, T2> action)
        {
            _eventCenter.Unsubscribe(name, action);
        }

        public static void Unsubscribe<T1, T2, T3>(string name, MAction<T1, T2, T3> action)
        {
            _eventCenter.Unsubscribe(name, action);
        }

        public static void SendEvent(string name)
        {
            _eventCenter.SendEvent(name);
        }

        public static void SendEvent<T>(string name, T info)
        {
            _eventCenter.SendEvent(name, info);
        }

        public static void SendEvent<T1, T2>(string name, T1 info1, T2 info2)
        {
            _eventCenter.SendEvent(name, info1, info2);
        }

        public static void SendEvent<T1, T2, T3>(string name, T1 info1, T2 info2, T3 info3)
        {
            _eventCenter.SendEvent(name, info1, info2, info3);
        }

        public static void Clear()
        {
            _eventCenter.Clear();
        }
    }

}