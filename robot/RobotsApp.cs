using Server3;

namespace robot
{

    public class RobotsApp : ServerApp
    {
        public RobotsApp() : base(AppType.Robot)
        {
        }

        public override void Init()
        {
            // for (int i = 0; i < 10; i++)
            // {
            //     Robot robot = new Robot("");
            //     _threadMgr.AddObjToThread(robot);
            // }

            RobotConsole robotConsole = new RobotConsole();
            _threadMgr.AddObjToThread(robotConsole);
        }
    }

}