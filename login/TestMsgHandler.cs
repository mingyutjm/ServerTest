using Server3;
using Server3.Message;

namespace login
{

    public class TestMsgHandler : ThreadObject
    {
        public override bool Init()
        {
            return true;
        }

        public override void Dispose()
        {
        }

        public override void RegisterMsgFunction()
        {
            // RegisterFunction((int)MsgId.TestMsg, HandleMsg);
        }

        public override void Tick()
        {
        }

        private void HandleMsg(Packet packet)
        {
            var obj = packet.Deserialize<TestMsg>();
            Log.Info(obj!.msg);
        }
    }

}