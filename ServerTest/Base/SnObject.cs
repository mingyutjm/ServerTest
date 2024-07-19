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

    public class TestAbc : IPoolObj<int>
    {
        public TestAbc()
        {
        }

        public void OnCreate(int arg)
        {
        }

        public void OnGet(int arg)
        {
        }

        public void OnRelease()
        {
        }

        public void Dispose()
        {
        }
    }

    public class TestMain()
    {
        public void Do()
        {
            TestAbc a = TSObj.Create<TestAbc, int>(1);
            TSObj.Destroy(a);
        }
    }

}