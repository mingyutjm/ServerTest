using Server3;

namespace login
{

    public class LoginApp : ServerApp
    {
        public LoginApp() : base(AppType.Login)
        {
        }

        public override void Init()
        {
            AddListenerToThread("127.0.0.1", 2233);
            _threadMgr.AddObjToThread(new TestMsgHandler());
        }
    }

}