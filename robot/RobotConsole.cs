using Server3;

namespace robot
{

    public class RobotConsole : GameConsole
    {
        public override bool Init()
        {
            base.Init();
            Register<RobotConsoleLogin>("login");
            return true;
        }
    }

    public class RobotConsoleLogin : GameConsoleCmd
    {
        public override void RegisterHandler()
        {
            RegisterHandler_Impl("-help", HandleHelp);
            RegisterHandler_Impl("-a", HandleLogin);
            RegisterHandler_Impl("-ex", HandleLoginEx);
        }

        private void HandleHelp(string p1, string p2)
        {
            Log.Info("-a account.\t\tlogin by account");
            Log.Info("-ex account count.\tbatch login, account is prefix, count as number");
        }

        private void HandleLogin(string p1, string p2)
        {
            Log.Info($"login. account: {p1}");
            Robot pRobot = new Robot(p1);
            ThreadMgr.Instance.AddObjToThread(pRobot);
        }

        private void HandleLoginEx(string p1, string p2)
        {
            if (int.TryParse(p2, out int count))
            {
                for (int i = 1; i <= count; i++)
                {
                    string account = $"{p1}{i}";
                    Robot pRobot = new Robot(account);
                    ThreadMgr.Instance.AddObjToThread(pRobot);
                    Log.Info($"login. account: {account}");
                }
            }
            else
            {
                Log.Error($"para 2 is not int");
            }
        }
    }

}