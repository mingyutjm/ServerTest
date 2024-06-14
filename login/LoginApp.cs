using Server3;

namespace login
{

    public class LoginApp : ServerApp
    {
        public LoginApp() : base(AppType.Login)
        {
        }

        public override void InitApp()
        {
            AddListenerToThread("127.0.0.1", 2233);
        }
    }

}