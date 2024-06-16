﻿using Server3;
using Server3.Message;

namespace robot
{

    public class Robot : NetworkConnector
    {
        private bool _isSendMsg = false;

        public override bool Init()
        {
            if (!base.Init())
                return false;
            Connect("127.0.0.1", 2233);
            return true;
        }

        public override void RegisterMsgFunction()
        {
            base.RegisterMsgFunction();
        }

        public override void Tick()
        {
            base.Tick();
            if (IsConnected() && !_isSendMsg)
            {
                TestMsg msg = new TestMsg();
                msg.msg = "robot msg";
                var packet = Packet.Create((int)MsgId.TestMsg, _masterSocket);
                packet.SerializeToBuffer(msg);
                SendPacket(packet);
                _isSendMsg = true;
            }
        }
    }

}