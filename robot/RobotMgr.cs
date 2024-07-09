using Server3;
using Server3.Message;

namespace robot
{

    public class RobotMgr : NetworkConnector
    {
        public override bool Init()
        {
            if (!base.Init())
                return false;
            Connect("127.0.0.1", 2233);
            return true;
        }

        public override void Tick()
        {
            base.Tick();
        }

        public override void RegisterMsgFunction()
        {
            var msgCallback = new MessageCallbackFunction();
            msgCallback.RegisterFunction((int)MsgId.RobotSyncState, HandleMsg_RobotState);
            AttachCallbackHandler(msgCallback);
        }

        private void HandleMsg_RobotState(Packet p)
        {
        }
    }

}