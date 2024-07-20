namespace Server3
{

    public class SnObject
    {
        protected ulong _sn;

        public ulong Sn => _sn;

        public SnObject()
        {
            _sn = Global.Instance.GenerateSn();
        }

        public SnObject(ulong sn)
        {
            _sn = sn;
        }
    }

}